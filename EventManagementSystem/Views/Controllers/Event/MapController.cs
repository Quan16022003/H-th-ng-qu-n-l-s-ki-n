using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Event.Controllers.Event
{
    [Area("Event")]
    public class MapController : Controller
    {
        private IPathProvider _pathProvider;

        public MapController([FromKeyedServices("Map")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        [HttpGet]
        public IActionResult Maps()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Maps.cshtml");
        }
    }
}
