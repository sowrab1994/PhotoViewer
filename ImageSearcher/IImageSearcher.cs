using System;
using System.Collections;

namespace ImageSearcher
{
    public class SearchResponse
    {
        public ArrayList imagesArray { get; set; }

        public bool IsRequestSuccessful { get; set; }

        public int ResponsePages { get; set; }

    }
    public interface IImageSearcher
    {
        void SetApiKey(string key = "");
        void SetPage(int page);
        SearchResponse GetImagesUrl(string text);
    }
}
