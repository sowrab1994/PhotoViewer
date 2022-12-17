using Browser;
using ImageSearcher;
using System;
using System.Collections;
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

        private string CurrentSearchItem;

        public SearchController(IBrowser browser, IImageSearcher imgSearcher) 
        { 
            this.browser = browser;
            this.imgSearcher = imgSearcher;
            callsToJs = new CallsToJs(this.browser);
        }

        public void QueryImages(string searchText)
        {
            callsToJs.ShowLoading();
            CurrentSearchItem = searchText;
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
    }
}
