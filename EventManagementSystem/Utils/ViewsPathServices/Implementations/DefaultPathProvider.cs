using Web.Controllers.Account;
using Web.Controllers.Event;
using Web.Controllers.Profile;

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
                nameof(ProfileController) => "~/Areas/Profile/Views/Profile",
                _ => throw new ArgumentException($"Does not found {target}")
            };
        }
    }
}
