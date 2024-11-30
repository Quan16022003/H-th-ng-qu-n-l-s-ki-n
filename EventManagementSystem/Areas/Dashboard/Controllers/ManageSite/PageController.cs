using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Controllers;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageSite
{
    [Authorize(Policy = "SiteManagement")]
    [Area("Dashboard")]
    public class PageController : BaseController
    {
        public PageController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<PageController>();
        }

        public IActionResult Index()
        {
            return View($"{ViewPath}/Pages.cshtml");
        }
    }
}
