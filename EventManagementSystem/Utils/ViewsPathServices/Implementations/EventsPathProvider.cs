using Web.Views.Controllers.Event;
using Web.Views.Controllers.Event;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class EventsPathProvider : IPathProvider
    {
        public string GetViewsPath(object target)
        {
            return target switch
            {
                EventsController => "~/Views/Event",
                _ => throw new ArgumentException($"Does not found {target.GetType().Name} in Account Area")
            };
        }
    }
}
