using Browser;
using ImageSearcher;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoViewer
{
    internal class SearchButtonClickedEventArgs : EventArgs
    {
        public ArrayList Results;
    }

    internal class SearchController
    {
        IBrowser browser;

        IImageSearcher imgSearcher;

        private EventHandler<SearchButtonClickedEventArgs> OnImagesResultsReceived;

        private string CurrentSearchItem;

        public SearchController(IBrowser browser, IImageSearcher imgSearcher) 
        { 
            this.browser = browser;
            this.imgSearcher = imgSearcher;     
        }

        public void QueryImages(string searchText)
        {
            CurrentSearchItem = searchText;
            OnImagesResultsReceived += SearchResultsReceivedHandler;
            Task.Run(() =>
            {
                ArrayList result = new ArrayList();
                result = imgSearcher.GetImagesUrl(searchText);
                var args = new SearchButtonClickedEventArgs();
                args.Results = result;
                OnImagesResultsReceived.Invoke(this, args);
            });
        }

        private void SearchResultsReceivedHandler(object sender, SearchButtonClickedEventArgs args)
        {
            OnImagesResultsReceived -= SearchResultsReceivedHandler;
            MessageBox.Show(args.Results.Count.ToString());

        }
    }
}
