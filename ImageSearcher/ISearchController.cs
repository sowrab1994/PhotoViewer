using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoViewer
{
    public interface ISearchController
    {
        Task QueryImages(string searchText, int pageNo = 1, bool newSearch = true);

        string QueryImagesOfPreviousPage();

        void QueryImagesOfNextPage();

        ConcurrentStack<Tuple<string, int>> GetSearchStack();

    }
}
