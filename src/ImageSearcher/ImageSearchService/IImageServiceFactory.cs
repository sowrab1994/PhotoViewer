namespace ImageSearcher.ImageSearchService
{
    internal interface IImageServiceFactory
    {
        /// <summary>
        ///     Returns the Image service from which we want to get images
        /// </summary>
        /// <returns></returns>
        IImageSearchService GetImageService();
    }
}
