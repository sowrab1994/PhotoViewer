using Browser;
using ImageSearcher;
using log4net;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoViewer
{
    internal class SearchButtonClickedEventArgs : EventArgs
    {
        public ArrayList imagesArray;

        public bool IsRequestSuccessful;

        public int NoOfPages;
    }

    internal class SearchController 
    {
        IBrowser browser;

        IImageSearcher imgSearcher;

        ICallsToJs callsToJs;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private EventHandler<SearchButtonClickedEventArgs> OnImagesResultsReceived;

        private ConcurrentStack<Tuple<string, int>> searchStack;

        public static EventHandler OnStackEmpty;

        public static EventHandler OnStackAdded;

        public int GetStackCount()
        {
            return searchStack.Count;
        }

        public SearchController(IBrowser browser, IImageSearcher imgSearcher, ICallsToJs callsToJs) 
        { 
            this.browser = browser;
            this.imgSearcher = imgSearcher;
            this.callsToJs = callsToJs;
            searchStack = new ConcurrentStack<Tuple<string, int>>();
        }

        public void QueryImages(string searchText, int pageNo = 1, bool newSearch = true)
        {
            Log.Info($"Search Query - {searchText}");
            callsToJs.ShowLoading();
                
            imgSearcher.SetPage(pageNo);
            
            if(newSearch) {
                searchStack.Push(new Tuple<string, int>(searchText, pageNo));
            }

            if (searchStack.Count > 1) {
                OnStackAdded?.Invoke(this, null);
            }

            OnImagesResultsReceived += SearchResultsReceivedHandler;

            Task.Run(() =>
            {
                var result = imgSearcher.GetImagesUrl(searchText);
                var args = new SearchButtonClickedEventArgs();
                args.IsRequestSuccessful = result.IsRequestSuccessful;
                args.imagesArray = result.imagesArray;
                args.NoOfPages = result.ResponsePages;
                OnImagesResultsReceived.Invoke(this, args);
            });
        }

        public string BackButtonHandler()
        {
            Tuple<string, int> deleteTuple, currentTuple;
            searchStack.TryPop(out deleteTuple);

            if (searchStack.TryPeek(out currentTuple))
            {
                QueryImages(currentTuple.Item1, currentTuple.Item2, newSearch:false);
                if(searchStack.Count <= 1)
                {
                    OnStackEmpty.Invoke(this, null);
                }
            }
            return currentTuple.Item1;
        }


        public void NextPageHandler()
        {
            Tuple<string, int> tuple;
            if (searchStack.TryPeek(out tuple))
            {
                QueryImages(tuple.Item1, tuple.Item2 + 1);
            }
        }

        private void SearchResultsReceivedHandler(object sender, SearchButtonClickedEventArgs args)
        {
            OnImagesResultsReceived -= SearchResultsReceivedHandler;
            if (args.IsRequestSuccessful)
            {
                if (args.imagesArray.Count > 0)
                {
                    callsToJs.LoadImages(args.imagesArray);
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
