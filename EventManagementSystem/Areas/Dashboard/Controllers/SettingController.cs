using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Controllers;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers
{
    [Authorize(Policy = "SystemConfiguration")]
    [Area("Dashboard")]
    public class SettingController : BaseController
    {
        public SettingController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<SettingController>();
        }

        public IActionResult Index()
        {
            LoadCurrentUser();
            return View($"{ViewPath}/Setting.cshtml");
        }
    }
}
