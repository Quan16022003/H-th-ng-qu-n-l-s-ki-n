namespace Web.Utils.ViewsPathServices
{
    public interface IPathProvider
    {
        /// <summary>
        /// Get folder path which contain the view
        /// </summary>
        /// <param name="target">Controller which handle the Views</param>
        /// <returns>Path to folder contain the view</returns>
        string GetViewsPath(object target);
    }
}
