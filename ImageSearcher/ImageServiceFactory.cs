using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearcher
{
    public class ImageServiceFactory : IImageServiceFactory
    {
        enum ImageServiceType
        {
            FlickerService
        }

        public IImageSearcher GetImageService()
        {
            var service = ImageServiceType.FlickerService;
            switch(service)
            {
                case ImageServiceType.FlickerService:
                    return new FlickerImageService();
                default:
                    return null;
            }
        }
    }
}
