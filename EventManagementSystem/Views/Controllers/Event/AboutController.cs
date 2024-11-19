using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Event.Controllers.Event
{
    [Area("Event")]
    public class AboutController : Controller
    {
        private IPathProvider _pathProvider;

        public AboutController([FromKeyedServices("About")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        [HttpGet]
        public IActionResult About()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/About.cshtml");
        }
    }
}
