using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class SettingController : Controller
    {
        private readonly string viewPath;

        public SettingController(IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<SettingController>("Dashboard");
        }

        public IActionResult Index()
        {
            return View($"{viewPath}/Setting.cshtml");
        }
    }
}
