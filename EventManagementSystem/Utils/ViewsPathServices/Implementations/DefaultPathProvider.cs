using Web.Controllers.Account;
using Web.Controllers.Event;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class DefaultPathProvider : IPathProvider
    {
        public string GetViewsPath(Type type)
        {
            string target = type.Name;
            return target switch
            {
                nameof(AccountController) => "~/Views/Account",
                nameof(EventsController) => "~/Views/Event",
                _ => throw new ArgumentException($"Does not found {target.GetType().Name} in Account Area")
            };
        }
    }
}
