using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Threading.Tasks;
using System.Windows;
using ProductConfig;

namespace Browser
{
    public class EdgeChromiumBrowser : IBrowser
    {
        private WebView2 edgeWebView;
        bool IsEdgeBrowserReady { get; set; }

        event EventHandler EdgeBrowserReadyEvent;

        public EdgeChromiumBrowser()
        {
            edgeWebView = new WebView2();

            edgeWebView.Loaded += async (sender, e) =>
            {
                await edgeWebView.EnsureCoreWebView2Async(null).ConfigureAwait(true);
                string path = System.IO.Path.Combine(@"C:\PhotoViewer\ReactModule", "build");
                edgeWebView.CoreWebView2.SetVirtualHostNameToFolderMapping("workspace.test", path,
                    CoreWebView2HostResourceAccessKind.Allow);
                edgeWebView.CoreWebView2.Settings.AreDevToolsEnabled = false;
                edgeWebView.CoreWebView2.Settings.IsBuiltInErrorPageEnabled = false;
                edgeWebView.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
                //edgeWebView.CoreWebView2.OpenDevToolsWindow();
                IsEdgeBrowserReady = true;
                EdgeBrowserReadyEvent?.Invoke(this, EventArgs.Empty);
            };
        }

        #region PublicMemberFunction
        public void LoadWebPage(string webPage)
        {
            this.edgeWebView.Dispatcher.Invoke(() =>
            {
                if (IsEdgeBrowserReady)
                {
                    edgeWebView.CoreWebView2.Navigate(webPage);
                }
                else
                {
                    EdgeBrowserReadyEvent += ((sender1, e1) =>
                    {
                        edgeWebView.CoreWebView2.Navigate(webPage);
                    });
                }
            });
        }

        public UIElement GetUIElement => (UIElement)this.edgeWebView;

        public void InvokeScriptAsync(string methodName, params object[] args)
        {
            string test = Config.BuildJavacriptFunction(methodName, args);
            this.edgeWebView.Dispatcher.Invoke(() =>
            {
                edgeWebView.ExecuteScriptAsync(test);
            });
        }
        #endregion
    }
}
