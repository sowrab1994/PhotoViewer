using Browser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductConfig;

namespace PhotoViewer
{
    public class CallsToJs : ICallsToJs
    {
        IBrowser browser;

        public CallsToJs(IBrowser browser) 
        { 
            this.browser = browser;
        }

        public void LoadImages(ArrayList images)
        {
            string imagesList = string.Join(";", (string[])images.ToArray(Type.GetType("System.String")));
            browser.InvokeScriptAsync(Config.LoadImages, new Object[] {imagesList});
        }

        public void ShowLoading()
        {
            browser.InvokeScriptAsync(Config.LoadingPage, new Object[] { });
        }

        public void ShowNoImages()
        {
            browser.InvokeScriptAsync(Config.EmptyResults,new Object[] { });
        }

        public void ShowNoInternet()
        {
            browser.InvokeScriptAsync(Config.LoadNoInternet, new Object[] { });
        }
    }
}
