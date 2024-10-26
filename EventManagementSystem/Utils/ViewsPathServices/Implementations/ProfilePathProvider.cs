using Web.Areas.Profile.Controllers;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class ProfilePathProvider : IPathProvider
    {
        public string GetViewsPath(object target)
        {
            return target switch
            {
                ProfileController => "~/Areas/Profile/Views/Profile",
                _ => throw new ArgumentException($"Does not found {target.GetType().Name} in Profile Area")
            };
        }
    }
}
