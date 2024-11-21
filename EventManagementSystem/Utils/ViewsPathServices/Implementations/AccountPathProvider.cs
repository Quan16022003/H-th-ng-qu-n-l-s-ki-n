using Web.Utils.ViewsPathServices.Implementations;
using Web.Views.Controllers.Event;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class AccountPathProvider : IPathProvider
    {
        public string GetViewsPath(object target)
        {
            return target switch
            {
                AccountController => "~/Views/Account",
                _ => throw new ArgumentException($"Does not found {target.GetType().Name} in Account Area")
            };
        }
    }
}
