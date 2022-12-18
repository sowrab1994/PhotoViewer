using System.Collections;

namespace Browser.JavaScriptCalls
{
    /// <summary>
    ///      Wrapper class to call various methods in the browser
    /// </summary>
    public interface ICallsToJs
    {
        /// <summary>
        ///      Calls the JS method to load the images
        /// </summary>
        /// <param name="images"></param>
        void LoadImages(ArrayList images);

        /// <summary>
        ///      Calls the JS method to show loading screen
        /// </summary>
        void ShowLoading();

        /// <summary>
        ///      Calls the JS Method to load No Internet page
        /// </summary>
        void ShowNoInternet();

        /// <summary>
        ///      Calls the JS method to show No Results page
        /// </summary>
        void ShowNoImages();
    }
}
