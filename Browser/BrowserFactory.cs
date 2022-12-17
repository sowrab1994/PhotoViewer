using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Browser
{
    enum BrowserType{
        Edge
    };

    public class BrowserFactory : IBrowserFactory
    {
        public IBrowser GetBrowser()
        {
            var browserType = BrowserType.Edge;
            switch (browserType)
            {
                case BrowserType.Edge:
                    return new EdgeChromiumBrowser();
                default:
                    return null;
            }
        }
    }
}
