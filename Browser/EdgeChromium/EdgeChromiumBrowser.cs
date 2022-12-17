using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace Browser
{
    public class EdgeChromiumBrowser : IBrowser
    {
        private WebView2 edgeWebView;
        //https://workspace.test/index.html
        private CoreWebView2Environment webView2Environment;
        bool IsEdgeBrowserReady { get; set; }

        event EventHandler EdgeBrowserReadyEvent;

        public EdgeChromiumBrowser()
        {
            edgeWebView = new WebView2();
            edgeWebView.CoreWebView2InitializationCompleted += OnInitializationCompleted;
            edgeWebView.NavigationStarting += OnNavigationStarting;
            edgeWebView.NavigationCompleted += OnNavigationCompleted;

            edgeWebView.Loaded += async (sender, e) =>
            {
                await edgeWebView.EnsureCoreWebView2Async(webView2Environment).ConfigureAwait(true);
                string path = System.IO.Path.Combine(@"C:\PhotoViewer", "Webpage");
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
        public async Task InitEnvironmentAsync()
        {
            var environmentOption = new CoreWebView2EnvironmentOptions();

            webView2Environment = await CoreWebView2Environment.CreateAsync(null,
                @"", environmentOption).ConfigureAwait(true);
        }


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

        private void OnNavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            Console.WriteLine("OnNavigationCompleted");
        }

        private void OnNavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            Console.WriteLine("OnNavigationStarting");
        }

        private void OnInitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            Console.WriteLine("Initialization completed");
        }
        

        public void InvokeScriptAsync(string methodName, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
