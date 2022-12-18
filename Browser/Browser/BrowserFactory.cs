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
