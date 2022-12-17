using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageSearcher
{
    internal interface IImageServiceFactory
    {
        IImageSearcher GetImageService();
    }
}
