using Web.Areas.Admin.Controllers;
using Web.Areas.Admin.Controllers.ManageBookings;
using Web.Areas.Admin.Controllers.ManageEvents;
using Web.Areas.Admin.Controllers.ManageSite;
using Web.Areas.Admin.Controllers.ManageUsers;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class AdminPathProvider : IPathProvider
    {
        public string GetViewsPath(object target)
        {
            return target switch
            {
                DashboardController => "~/Areas/Admin/Views/Dashboard",
                ContactController => "~/Areas/Admin/Views/Contact",
                SettingController => "~/Areas/Admin/Views/Setting",
                BookingController => "~/Areas/Admin/Views/ManageBookings",
                CategoryController or EventController => "~/Areas/Admin/Views/ManageEvents",
                BannerController or MediaController or PageController => "~/Areas/Admin/Views/ManageSite",
                UserController => "~/Areas/Admin/Views/ManageUsers",
                _ => throw new ArgumentException($"Does not found {target.GetType().Name} in Admin Area")
            };
        }
    }
}
