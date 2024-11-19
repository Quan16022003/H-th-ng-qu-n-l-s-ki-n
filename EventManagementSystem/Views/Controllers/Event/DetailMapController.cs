using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Event.Controllers.Event
{
    [Area("Event")]
    public class DetailMapController : Controller
    {
        private IPathProvider _pathProvider;

        public DetailMapController([FromKeyedServices("DetailMap")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        [HttpGet]
        public IActionResult DetailMap()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/DetailMap.cshtml");
        }
    }
}
