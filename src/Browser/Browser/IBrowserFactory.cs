namespace Browser
{
    internal interface IBrowserFactory
    {
        /// <summary>
        ///     Returns the browser Instance based on type of browser
        /// </summary>
        /// <returns></returns>
        IBrowser GetBrowser();
    }
}
