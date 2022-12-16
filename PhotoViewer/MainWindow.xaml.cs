using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace PhotoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WebView2 edgeWebView;
        private CoreWebView2Environment webView2Environment;
        bool IsEdgeBrowserReady { get; set; }
        event EventHandler EdgeBrowserReadyEvent;

        public async Task InitEnvironmentAsync()
        {
            var environmentOption = new CoreWebView2EnvironmentOptions();

            webView2Environment = await CoreWebView2Environment.CreateAsync(null,
                @"", environmentOption).ConfigureAwait(true);
        }

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            LoadUI();
            LoadURL();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SearchButton.IsEnabled = false;
        }


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

        public void LoadUI()
        {
            edgeWebView = new WebView2();
            edgeWebView.CoreWebView2InitializationCompleted += OnInitializationCompleted;
            edgeWebView.NavigationStarting += OnNavigationStarting;
            edgeWebView.NavigationCompleted += OnNavigationCompleted;
            WebPageLoader.Children.Add(edgeWebView);

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

        public void LoadURL()
        {
            this.edgeWebView.Dispatcher.Invoke(() =>
            {
                if (IsEdgeBrowserReady)
                {
                    edgeWebView.CoreWebView2.Navigate("https://workspace.test/index.html");
                }
                else
                {
                    EdgeBrowserReadyEvent += ((sender1, e1) =>
                    {
                        edgeWebView.CoreWebView2.Navigate("https://workspace.test/index.html");
                    });
                }
            });
        }

        private void TextBoxChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            SearchButton.IsEnabled = SearchTextBox.Text.Length > 0;
            
        }

        private void OnSearchButtonClicked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
