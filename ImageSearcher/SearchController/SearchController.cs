using Browser;
using log4net;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading.Tasks;
using Browser.JavaScriptCalls;

namespace ImageSearcher
{
    internal class SearchButtonClickedEventArgs : EventArgs
    {
        public ArrayList imagesArray;

        public bool IsRequestSuccessful;

        public int NoOfPages;
    }

    public class SearchController : ISearchController
    {

        #region Members
        IBrowser browser;

        IImageSearchService imgSearcher;

        ICallsToJs callsToJs;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private ConcurrentStack<Tuple<string, int>> searchStack;

        #endregion

        #region EventHandler

        private EventHandler<SearchButtonClickedEventArgs> OnImagesResultsReceived;

        public static EventHandler OnStackEmpty;

        public static EventHandler OnStackAdded;

        #endregion

        #region PublicMemberFunction

        public SearchController(IBrowser browser, IImageSearchService imgSearcher, ICallsToJs callsToJs) 
        { 
            this.browser = browser;
            this.imgSearcher = imgSearcher;
            this.callsToJs = callsToJs;
            searchStack = new ConcurrentStack<Tuple<string, int>>();
        }

        public Task QueryImages(string searchText, int pageNo = 1, bool newSearch = true)
        {
            Log.Info($"Search Query - {searchText}, Page No - {pageNo}");
            callsToJs.ShowLoading();
                
            imgSearcher.SetPage(pageNo);
            
            if(newSearch) {
               Log.Info("Query Initiated from Search Box, pushing to stack");
               searchStack.Push(new Tuple<string, int>(searchText, pageNo));
            }

            if (searchStack.Count > 1) {
                OnStackAdded?.Invoke(this, null);
            }

            OnImagesResultsReceived += SearchResultsReceivedHandler;

            Task t = Task.Run(() =>
            {
                var result = imgSearcher.GetImagesForSearchString(searchText);
                var args = new SearchButtonClickedEventArgs();
                args.IsRequestSuccessful = result.IsRequestSuccessful;
                args.imagesArray = result.ImagesArray;
                args.NoOfPages = result.ResponsePages;
                OnImagesResultsReceived?.Invoke(this, args);
            });
            return t;
        }

        public string QueryImagesOfPreviousPage()
        {
            Log.Info("Querying images of previous page");
            Tuple<string, int> deleteTuple, currentTuple;
            searchStack.TryPop(out deleteTuple);

            if (searchStack.TryPeek(out currentTuple))
            {
                QueryImages(currentTuple.Item1, currentTuple.Item2, newSearch:false);
                if(searchStack.Count <= 1)
                {
                    OnStackEmpty.Invoke(this, null);
                }
                return currentTuple.Item1;
            }
            return string.Empty;
        }


        public void QueryImagesOfNextPage()
        {
            Log.Info("Querying images of next page");
            Tuple<string, int> tuple;
            if (searchStack.TryPeek(out tuple))
            {
                QueryImages(tuple.Item1, tuple.Item2 + 1);
            }
        }

        public ConcurrentStack<Tuple<string, int>> GetSearchStack()
        {
            return searchStack;
        }

        #endregion

        #region privateMethods

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
        #endregion

    }
}
