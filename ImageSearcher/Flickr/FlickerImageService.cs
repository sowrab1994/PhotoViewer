using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductConfig;
using ImageSearcher.HttpRequest;

namespace ImageSearcher.Flicker
{
    public class FlickerImageService : IImageSearchService
    {
        private Dictionary<string, string> urlQueryDict  = new Dictionary<string, string>();
        IHttpRequestService httpRequest;
        FlickerResponseProcessor processor;

        public FlickerImageService(IHttpRequestService httpRequestService) 
        {
            httpRequest = httpRequestService;
            processor = new FlickerResponseProcessor();
        }

        #region PrivateMethods
        private string MergeKeyValue(string key, string val)
        {
            return "&" + key + "=" + val;
        }

        private void FormUrl(ref string url)
        {
            foreach (var ele in urlQueryDict)
            {
                url += MergeKeyValue(ele.Key, ele.Value);
            }
        }

        #endregion

        #region PublicMemberFunction
        public SearchResponse GetImagesForSearchString(string text)
        {
            string url = Config.urlPrefix + "method" + "=" + Config.searchMethodName;
            SearchResponse searchResponse = new SearchResponse();
            SetApiKey();
            SetText(text);
            FormUrl(ref url);
            Task<string> tsk = null;
            bool isRequestSuccessful = true;
            Task.Run(() =>
            {
                tsk = httpRequest.GetHttpResponse(url);  
            }).Wait();
            var responseArray = new ArrayList();
            string responseString = string.Empty;
            try
            {
                responseString = tsk.Result.ToString();
            }
            catch
            {
                isRequestSuccessful = false;
            }


            if (!String.IsNullOrEmpty(responseString))
            {
                try
                {
                    if (processor.SerializeResponse(responseString))
                    {
                        searchResponse.ImagesArray = processor.GetImagesUrl();
                        searchResponse.ResponsePages = processor.GetResponsePages();
                    }
                }
                catch
                {
                    //Log here
                }
            }
            searchResponse.IsRequestSuccessful = isRequestSuccessful; 

            return searchResponse;
        }

        private void SetText(string text)
        {
            urlQueryDict["text"] = text.ToString();
        }

        public void SetApiKey(string key = "")
        {
            if(String.IsNullOrEmpty(key))
            {
                urlQueryDict["api_key"] = Config.apiKey;
            }
            else
            {
                urlQueryDict["api_key"] = key;
            }
        }

        public void SetPage(int page)
        {
            urlQueryDict["page"] = page.ToString();
        }
        #endregion
    }
}
