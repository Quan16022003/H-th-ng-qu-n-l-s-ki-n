using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class StatisticsController : Controller
    {
        private readonly string viewPath;

        public StatisticsController(IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<StatisticsController>("Dashboard");
        }

        public IActionResult Index()
        {
            return View($"{viewPath}/Statistics.cshtml");
        }
    }
}
