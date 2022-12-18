using ProductConfig;
using ImageSearcher.HttpRequest;
using ImageSearcher.Flicker;

namespace ImageSearcher.ImageSearchService
{
    public class ImageServiceFactory : IImageServiceFactory
    {
        
        public IImageSearchService GetImageService()
        {
            var service = Config.ReadRegValueString("Software\\PhotoViewer", "serviceName");
            switch(service.ToLower())
            {
                case "flickr":
                    return new FlickerImageService(HttpRequestService.GetInstance());
                default:
                    return new FlickerImageService(HttpRequestService.GetInstance());
            }
        }
    }
}
