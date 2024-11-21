using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Controllers;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class StatisticsController : BaseController
    {
        public StatisticsController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<StatisticsController>();
        }

        public IActionResult Index()
        {
            LoadCurrentUser();
            return View($"{ViewPath}/Statistics.cshtml");
        }
    }
}
