using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageSite
{
    [Area("Dashboard")]
    public class MediaController : Controller
    {
        private readonly string viewPath;

        public MediaController(IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<MediaController>("Dashboard");
        }

        public IActionResult Index()
        {
            return View($"{viewPath}/Media.cshtml");
        }
    }
}
