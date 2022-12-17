using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace ImageSearcher
{
    public class FlickerImageService : IImageSearcher
    {
        private string apiKey = "c098ab4dc93e9a203a007ad613d1c414";
        private string urlPrefix = "https://www.flickr.com/services/rest/?";
        private string methodName = "flickr.photos.search";
        private string url;
        private Dictionary<string, string> urlQueryDict  = new Dictionary<string, string>();
        HttpRequestService httpRequest;
        FlickerResponseProcessor processor;

        public FlickerImageService() 
        {
            url = url + urlPrefix + "method" + "=" + methodName;
            httpRequest = HttpRequestService.GetInstance();
            processor = new FlickerResponseProcessor();
        }

        private string MergeKeyValue(string key, string val)
        {
            return "&" + key + "=" + val;
        }

        private void FormUrl()
        {
            foreach (var ele in urlQueryDict)
            {
                url += MergeKeyValue(ele.Key, ele.Value);
            }
        }


        public async Task<ArrayList> GetImagesUrl(string text)
        {
            SetApiKey();
            SetText(text);
            FormUrl();

            Task<ArrayList> task = Task.Run(() =>
            {
                ArrayList imagesList = new ArrayList();
                httpRequest.GetHttpResponse(url)
                    .ContinueWith(t =>
                    {
                        string responseString = t.Result.ToString();
                        if (!String.IsNullOrEmpty(responseString))
                        {
                            processor.SerializeResponse(responseString);
                            imagesList = processor.GetImagesUrl();
                        }

                    });
                return imagesList;
            });
            return await Task.FromResult(task.Result);
        }

        private void SetText(string text)
        {
            urlQueryDict["text"] = text.ToString();
        }

        public void SetApiKey(string key = "")
        {
            if(String.IsNullOrEmpty(key))
            {
                urlQueryDict["api_key"] = apiKey;
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
    }
}
