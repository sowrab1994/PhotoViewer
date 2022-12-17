using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Browser;
using ImageSearcher;
using ProductConfig;

namespace PhotoViewer
{

   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBrowser browser;

        IImageSearcher imgSearcher;

        SearchController controller;

        ICallsToJs callToJs;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            browser = new BrowserFactory().GetBrowser();
            imgSearcher = new ImageServiceFactory().GetImageService();
            callToJs = new CallsToJs(browser);
            controller = new SearchController(browser, imgSearcher, callToJs);
            WebPageLoader.Children.Add(browser.GetUIElement);
            browser.LoadWebPage(Config.webPageUrl);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SearchButton.IsEnabled = false;
            SearchButton.Foreground = new SolidColorBrush(Colors.Black); 
            BackButton.Visibility = Visibility.Collapsed;
            SearchController.OnStackAdded += StackAddedEventHandler;
            SearchController.OnStackEmpty += StackEmptyEventHanlder;
        }

        private void StackEmptyEventHanlder(object sender, EventArgs e)
        {
            BackButton.Visibility = Visibility.Collapsed;
        }

        private void StackAddedEventHandler(object sender, EventArgs e)
        {
            BackButton.Visibility = Visibility.Visible;
        }

        private void TextBoxChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            SearchButton.IsEnabled = SearchTextBox.Text.Length > 0;
            SearchButton.Foreground = SearchButton.IsEnabled ?
                new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
        }

        private void OnSearchButtonClicked(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text;
            controller.QueryImages(searchText);
        }


        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            var str = controller.BackButtonHandler();
            if (!string.IsNullOrWhiteSpace(str))
            {
                SearchTextBox.Text = str;
            }
        }
    }
}
