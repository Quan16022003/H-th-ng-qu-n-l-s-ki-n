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
    public class MediaController : BaseController
    {
        public MediaController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<MediaController>();
        }

        public IActionResult Index()
        {
            LoadCurrentUser();
            return View($"{ViewPath}/Media.cshtml");
        }
    }
}
