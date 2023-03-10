using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Browser;
using ImageSearcher;
using log4net;
using ProductConfig;
using Browser.JavaScriptCalls;
using ImageSearcher.ImageSearchService;

namespace PhotoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBrowser browser;

        private static ILog Log;

        ISearchController controller;

        private object backButtonlockobj = new object();
        private object searcButtonlockobj = new object();


        public MainWindow()
        {
            InitializeComponent();
            IntializeLogging();
            Loaded += MainWindow_Loaded;
            browser = new BrowserFactory().GetBrowser();
            controller = new SearchController(new ImageServiceFactory().GetImageService()
                , new CallsToJs(browser));
            WebPageLoader.Children.Add(browser.GetUIElement);
            browser.LoadWebPage(Config.webPageUrl);
        }

        private void IntializeLogging()
        {
            try
            {
                var logFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "PhotoViewer", "Logs");
                if (!Directory.Exists(logFolderPath)) Directory.CreateDirectory(logFolderPath);
                var logFilePath = Path.Combine(logFolderPath, "Applicationlog.log");
                GlobalContext.Properties["LogFilePath"] = logFilePath;
                log4net.Config.XmlConfigurator.Configure();
                Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                Log.Info("Logging initialized successfully.");
            }
            catch { }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SearchButton.IsEnabled = false;
            SearchButton.Foreground = new SolidColorBrush(Colors.Black); 
            BackButton.Visibility = Visibility.Collapsed;
            
            SearchController.OnStackAdded += StackAddedEventHandler;
            SearchController.OnStackEmpty += StackEmptyEventHanlder;

            Log.Info("MainWindow Loaded");
        }

        private void StackEmptyEventHanlder(object sender, EventArgs e)
        {
            Log.Info("Disabling back button");
            lock (backButtonlockobj)
            {
                BackButton.Visibility = Visibility.Collapsed;
            }
        }

        private void StackAddedEventHandler(object sender, EventArgs e)
        {
            lock (backButtonlockobj)
            {
                Log.Info("Enabling back button");
                if (BackButton.Visibility != Visibility.Visible)
                {
                    BackButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void TextBoxChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            lock (searcButtonlockobj)
            {
                SearchButton.IsEnabled = SearchTextBox.Text.Length > 0;
                SearchButton.Foreground = SearchButton.IsEnabled ?
                    new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
            }
        }

        private void OnSearchButtonClicked(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            controller.QueryImages(searchText);
        }


        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            var str = controller.QueryImagesOfPreviousPage();
            if (!string.IsNullOrWhiteSpace(str))
            {
                SearchTextBox.Text = str;
            }
        }

        private void OnNextButtonClicked(object sender, RoutedEventArgs e)
        {
            controller.QueryImagesOfNextPage();
        }

        private void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Return)
            {
                string searchText = SearchTextBox.Text;
                controller.QueryImages(searchText);
            }
        }
    }
}
