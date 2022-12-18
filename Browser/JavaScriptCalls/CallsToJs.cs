using Browser;
using System;
using System.Collections;
using ProductConfig;
using System.Reflection;
using log4net;

namespace Browser.JavaScriptCalls
{
    public class CallsToJs : ICallsToJs
    {
        IBrowser browser;

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CallsToJs(IBrowser browser) 
        { 
            this.browser = browser;
        }

        #region PublicMemberFunction
        public void LoadImages(ArrayList images)
        {
            Log.Info("Loading the Images in the browser");
            string imagesList = string.Join(";", (string[])images.ToArray(Type.GetType("System.String")));
            browser.InvokeScriptAsync(Config.LoadImages, new Object[] {imagesList});
        }

        public void ShowLoading()
        {
            Log.Info("Showing LoadingPage in the browser");
            browser.InvokeScriptAsync(Config.LoadingPage, new Object[] { });
        }

        public void ShowNoImages()
        {
            Log.Info("Showing No search results in the browser");
            browser.InvokeScriptAsync(Config.LoadNoResultsPage,new Object[] { });
        }

        public void ShowNoInternet()
        {
            Log.Info("Showing No Internet erro in the browser");
            browser.InvokeScriptAsync(Config.LoadNoInternet, new Object[] { });
        }
        #endregion
    }
}
