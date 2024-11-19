using Web.Areas.Event.Controllers.Event;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class DetailPathProvider : IPathProvider
    {
        public string GetViewsPath(object target)
        {
            return target switch
            {
                DetailsController => "~~/Views/Event",
                _ => throw new ArgumentException($"Does not found {target.GetType().Name} in Account Area")
            };
        }
    }
}
