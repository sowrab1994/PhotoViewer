using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImageSearcher
{
    public class HttpRequestService 
    {
        private readonly HttpClient client;
        private static object lockObj = new object();

        private static HttpRequestService httpRequestSrv;

        private HttpRequestService()
        {
            client = new HttpClient();
        }

        public static HttpRequestService GetInstance()
        { 
            if(httpRequestSrv == null)
            {
                lock (lockObj)
                {
                    httpRequestSrv = new HttpRequestService();
                }
            }
            return httpRequestSrv;
            
        }

        public async Task<string> GetHttpResponse(string url)
        {
            
            var response = string.Empty;
            try
            {
                HttpResponseMessage result = await client.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            } 
            catch (Exception ex)
            {
                response = string.Empty;
            }
            return response;
        }
    }
}
