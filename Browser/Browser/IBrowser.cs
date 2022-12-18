using System.Windows;

namespace Browser
{
    /// <summary>
    ///     Class responsible for loading the webpage in wpf
    /// </summary>
    public interface IBrowser
    {
        /// <summary>
        ///    Loads the webpage given the url
        /// </summary>
        /// <param name="webpageUrl"></param>
        void LoadWebPage(string webpageUrl);

        /// <summary>
        ///     Invokes the Javascript method running on browser
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        void InvokeScriptAsync(string methodName, params object[] args);

        /// <summary>
        ///     Gets UI Element of the browser
        /// </summary>
        UIElement GetUIElement { get; }
    }
}
