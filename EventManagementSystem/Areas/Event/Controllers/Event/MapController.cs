using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Event.Controllers.Event
{
    [Area("Map")]
    public class MapController : Controller
    {
        [HttpGet]
        public IActionResult Map()
        {
            return View();
        }
    }
}
