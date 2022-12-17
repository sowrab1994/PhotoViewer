using Browser;
using ImageSearcher;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoViewer
{
    internal class SearchButtonClickedEventArgs : EventArgs
    {
        public ArrayList Results;

        public bool IsRequestSuccessful;
    }

    internal class SearchController 
    {
        IBrowser browser;

        IImageSearcher imgSearcher;

        ICallsToJs callsToJs;

        private EventHandler<SearchButtonClickedEventArgs> OnImagesResultsReceived;

        private ConcurrentStack<string> searchStack;

        public static EventHandler OnStackEmpty;

        public static EventHandler OnStackAdded;

        public string currentSearch;

        public int GetStackCount()
        {
            return searchStack.Count;
        }

        public SearchController(IBrowser browser, IImageSearcher imgSearcher, ICallsToJs callsToJs) 
        { 
            this.browser = browser;
            this.imgSearcher = imgSearcher;
            this.callsToJs = callsToJs;
            searchStack = new ConcurrentStack<string>();
            currentSearch = string.Empty;

        }

        public void QueryImages(string searchText)
        {
            currentSearch = searchText;
            callsToJs.ShowLoading();
            
            searchStack.Push(searchText);
            
            if(searchStack.Count > 1) {
                OnStackAdded?.Invoke(this, null);
            }

            OnImagesResultsReceived += SearchResultsReceivedHandler;
            Task.Run(() =>
            {
                bool isRequestSuccess;
                var result = imgSearcher.GetImagesUrl(searchText, out isRequestSuccess);
                var args = new SearchButtonClickedEventArgs();
                args.IsRequestSuccessful = isRequestSuccess;
                args.Results = result;
                OnImagesResultsReceived.Invoke(this, args);
            });
        }

        private void SearchResultsReceivedHandler(object sender, SearchButtonClickedEventArgs args)
        {
            OnImagesResultsReceived -= SearchResultsReceivedHandler;
            if (args.IsRequestSuccessful)
            {
                if(args.Results.Count > 0) 
                {
                    callsToJs.LoadImages(args.Results);
                }
                else
                {
                    callsToJs.ShowNoImages();
                }
            }
            else
            {
                callsToJs.ShowNoInternet();
            }
        }


        public string BackButtonHandler()
        {
            var searchString = string.Empty;
            searchStack.TryPop(out searchString);
            if(currentSearch == searchString)
            {
                searchStack.TryPop(out searchString);
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                QueryImages(searchString);
                if(searchStack.Count <= 1)
                {
                    OnStackEmpty.Invoke(this, null);
                }
            }
            return searchString;
        }
    }
}
