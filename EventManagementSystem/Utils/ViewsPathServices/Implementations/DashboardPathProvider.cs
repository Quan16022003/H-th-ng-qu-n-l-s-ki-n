using Web.Areas.Dashboard.Controllers;
using Web.Areas.Dashboard.Controllers.ManageBookings;
using Web.Areas.Dashboard.Controllers.ManageEvents;
using Web.Areas.Dashboard.Controllers.ManageSite;
using Web.Areas.Dashboard.Controllers.ManageUsers;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class DashboardPathProvider : IPathProvider
    {
        public string GetViewsPath(Type type)
        {
            string target = type.Name;
            return target switch
            {
                nameof(StatisticsController) => "~/Areas/Dashboard/Views/Statistics",
                nameof(ContactController) => "~/Areas/Dashboard/Views/Contact",
                nameof(SettingController) => "~/Areas/Dashboard/Views/Setting",
                nameof(BookingController) => "~/Areas/Dashboard/Views/ManageBookings",
                nameof(CategoryController) => "~/Areas/Dashboard/Views/ManageEvents/Category",
                nameof(EventController) => "~/Areas/Dashboard/Views/ManageEvents/Event",
                nameof(BannerController) or nameof(MediaController) or nameof(PageController) 
                    => "~/Areas/Dashboard/Views/ManageSite",
                nameof(UserController) => "~/Areas/Dashboard/Views/ManageUsers",
                _ => throw new ArgumentException($"Does not found {target} in Admin Area")
            };
        }
    }
}
