using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Controllers;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageSite
{
    [Area("Dashboard")]
    public class BannerController : BaseController
    {
        public BannerController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<BannerController>();
        }

        public IActionResult Index()
        {
            LoadCurrentUser();
            return View($"{ViewPath}/Banners.cshtml");
        }
    }
}
