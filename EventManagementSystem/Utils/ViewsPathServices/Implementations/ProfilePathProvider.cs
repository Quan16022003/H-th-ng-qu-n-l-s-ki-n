using Web.Areas.Profile.Controllers;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class ProfilePathProvider : IPathProvider
    {
        public string GetViewsPath(Type type)
        {
            string target = type.Name;
            return target switch
            {
                nameof(ProfileController) => "~/Areas/Profile/Views/Profile",
                _ => throw new ArgumentException($"Does not found {target} in Profile Area")
            };
        }
    }
}
