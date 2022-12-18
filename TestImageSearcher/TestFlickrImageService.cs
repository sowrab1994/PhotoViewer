using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageSearcher;
using NUnit.Framework;

namespace TestImageSearcher
{
    internal class TestFlickrImageService
    {
        IImageSearchService searcher;
        [SetUp]
        public void SetUp() 
        {
            searcher = new FlickerImageService();
        }
    }
}
