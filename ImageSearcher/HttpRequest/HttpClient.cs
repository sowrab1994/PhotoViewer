using log4net;
using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace ImageSearcher.HttpRequest
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly HttpClient client;
        private static object lockObj = new object();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static HttpRequestService httpRequestSrv;

        private HttpRequestService()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(2);
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
                Log.Info($"Makeing http request with the {url}");
                HttpResponseMessage result = await client.GetAsync(url);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                response = string.Empty;
                throw ex;
            }
            return response;
        }
    }
}
