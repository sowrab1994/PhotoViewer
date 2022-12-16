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
    internal class HttpRequest 
    {
        private readonly HttpClient client;
        HttpRequest()
        {
            client = new HttpClient();
        }

        public async Task<string> GetHttpResponse(string url)
        {
            var response = string.Empty;
            HttpResponseMessage result = await client.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                response = await result.Content.ReadAsStringAsync();
            }
            return response;
        }
    }
}
