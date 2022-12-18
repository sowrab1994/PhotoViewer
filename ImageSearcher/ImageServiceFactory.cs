using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProductConfig;

namespace ImageSearcher
{
    public class ImageServiceFactory : IImageServiceFactory
    {
        
        public IImageSearchService GetImageService()
        {
            var service = Config.ReadRegValueString("Software\\PhotoViewer", "serviceName");
            switch(service.ToLower())
            {
                case "flickr":
                    return new FlickerImageService();
                default:
                    return new FlickerImageService();
            }
        }
    }
}
