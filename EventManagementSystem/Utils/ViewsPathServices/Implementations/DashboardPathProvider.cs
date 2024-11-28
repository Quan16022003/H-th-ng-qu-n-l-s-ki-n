using Web.Areas.Dashboard.Controllers;
using Web.Areas.Dashboard.Controllers.ManageBookings;
using Web.Areas.Dashboard.Controllers.ManageEvents;
using Web.Areas.Dashboard.Controllers.ManageSite;
using Web.Areas.Dashboard.Controllers.ManageUsers;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class DashboardPathProvider : IPathProvider
    {
        private readonly string folder = "~/Areas/Dashboard/Views";

        public string GetViewsPath(Type type)
        {
            string target = type.Name;
            return target switch
            {
                nameof(AccountController) => $"{folder}/Account",
                nameof(StatisticsController) => $"{folder}/Statistics",
                nameof(ContactController) => $"{folder}/Contact",
                nameof(SettingController) => $"{folder}/Setting",
                nameof(BookingController) => $"{folder}/ManageBookings",
                nameof(CategoryController) => $"{folder}/ManageEvents/Category",
                nameof(EventController) => $"{folder}/ManageEvents/Event",
                nameof(BannerController) or nameof(MediaController) or nameof(PageController) 
                    => $"{folder}/ManageSite",
                nameof(UserController) => $"{folder}/ManageUsers",
                _ => throw new ArgumentException($"Does not found {target} in Dashboard Area")
            };
        }
    }
}
