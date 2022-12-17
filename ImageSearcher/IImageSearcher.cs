using System;
using System.Collections;

namespace ImageSearcher
{
    public interface IImageSearcher
    {
        void SetApiKey(string key = "");
        void SetPage(int page);
        ArrayList GetImagesUrl(string text);
    }
}
