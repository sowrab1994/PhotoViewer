using System.Threading.Tasks;

namespace ImageSearcher.HttpRequest
{
    public interface IHttpRequestService
    {
        /// <summary>
        ///     Make HTTP GET Request for requested url and returns the task to await
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<string> GetHttpResponse(string url);
    }
}