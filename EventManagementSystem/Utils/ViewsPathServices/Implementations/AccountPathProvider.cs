using Web.Areas.Profile.Controllers;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class AccountPathProvider : IPathProvider
    {
        public string GetViewsPath(object target)
        {
            return target switch
            {
                AccountController or RegisterController => "~/Areas/Account/Views/Account",
                _ => throw new ArgumentException($"Does not found {target.GetType().Name} in Account Area")
            };
        }
    }
}
