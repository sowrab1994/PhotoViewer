using System.Windows;
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

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            browser = new BrowserFactory().GetBrowser();
            imgSearcher = new ImageServiceFactory().GetImageService();
            WebPageLoader.Children.Add(browser.GetUIElement);
            browser.LoadWebPage(Config.webPageUrl);

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SearchButton.IsEnabled = false;
        }



        private void TextBoxChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

            SearchButton.IsEnabled = SearchTextBox.Text.Length > 0;
            
        }

        private void OnSearchButtonClicked(object sender, RoutedEventArgs e)
        {
            imgSearcher.GetImagesUrl(SearchTextBox.Text);
        }
    }
}
