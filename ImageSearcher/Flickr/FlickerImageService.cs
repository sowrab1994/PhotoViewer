﻿using System;
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

       
        public ArrayList GetImagesUrl(string text, out bool IsRequestSuccess)
        {
            SetApiKey();
            SetText(text);
            FormUrl();
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
                    processor.SerializeResponse(responseString);
                    responseArray = processor.GetImagesUrl();
                }
                catch
                {
                    //Log here
                }
            }
            IsRequestSuccess = isRequestSuccessful;
            return responseArray;
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
