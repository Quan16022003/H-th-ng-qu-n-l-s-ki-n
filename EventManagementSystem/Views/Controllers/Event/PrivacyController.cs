using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Event.Controllers.Event
{
    [Area("Event")]
    public class PrivacyController : Controller
    {
        private IPathProvider _pathProvider;

        public PrivacyController([FromKeyedServices("Privacy")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Privacy.cshtml");
        }
    }
}
