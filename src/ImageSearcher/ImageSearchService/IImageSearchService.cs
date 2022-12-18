using System;
using System.Collections;

namespace ImageSearcher
{
    public class SearchResponse
    {   
        public SearchResponse() { 
            ImagesArray = new ArrayList();
            ResponsePages = 0;
            IsRequestSuccessful= false; 
        }
        public ArrayList ImagesArray { get; set; }

        public bool IsRequestSuccessful { get; set; }

        public int ResponsePages { get; set; }

    }
    
    /// <summary>
    ///     Responsible for Forming the url and 
    ///     making HTTP request.
    /// </summary>
    public interface IImageSearchService
    {
        /// <summary>
        ///     Sets API key required for the application
        /// </summary>
        /// <param name="key"></param>
        void SetApiKey(string key = "");

        /// <summary>
        ///     Sets Page number for which requests needs to be made
        /// </summary>
        /// <param name="page"></param>
        void SetPage(int page);

        /// <summary>
        ///     Forms the URL and makes HTTP request to get Images list
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Class which consists of search results</returns>
        SearchResponse GetImagesForSearchString(string text);
    }
}
