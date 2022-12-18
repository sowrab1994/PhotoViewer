using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ImageSearcher
{
    public interface ISearchController
    {
        /// <summary>
        ///      Queries the Imager Service Provider and returns a task which consumer can 
        ///      wait for. 
        /// </summary>
        /// <param name="searchText">Image that needs to be searched</param>
        /// <param name="pageNo">[Optional] Set to fetch returns from page no specified</param>
        /// <param name="newSearch">[Optional] Whether the search was initaited by new string</param>
        /// <returns></returns>
        Task QueryImages(string searchText, int pageNo = 1, bool newSearch = true);


        /// <summary>
        ///     Queries the images of previous seach result
        /// </summary>
        /// <returns></returns>
        string QueryImagesOfPreviousPage();

        /// <summary>
        ///    Fetches the result of next page in search result
        /// </summary>
        void QueryImagesOfNextPage();

        /// <summary>
        ///    Returns the stack of search results
        /// </summary>
        /// <returns></returns>
        ConcurrentStack<Tuple<string, int>> GetSearchStack();

    }
}
