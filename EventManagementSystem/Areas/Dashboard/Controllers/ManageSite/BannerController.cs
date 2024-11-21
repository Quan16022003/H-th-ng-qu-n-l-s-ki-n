using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageSite
{
    [Area("Dashboard")]
    public class BannerController : Controller
    {
        private readonly string viewPath;

        public BannerController(IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<BannerController>();
        }

        public IActionResult Index()
        {
            return View($"{viewPath}/Banners.cshtml");
        }
    }
}
