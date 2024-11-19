using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageSite
{
    [Area("Dashboard")]
    public class PageController : Controller
    {
        private readonly string viewPath;

        public PageController(IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<PageController>("Dashboard");
        }

        public IActionResult Index()
        {
            return View($"{viewPath}/Pages.cshtml");
        }
    }
}
