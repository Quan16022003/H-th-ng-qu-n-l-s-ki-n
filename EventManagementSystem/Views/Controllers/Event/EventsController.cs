using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;


namespace Web.Views.Controllers.Event
{
    [Area("Event")]
    public class EventsController : Controller
    {
        private IPathProvider _pathProvider;

        public EventsController([FromKeyedServices("Events")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }
        [HttpGet]
        public IActionResult AllEvent()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/AllEvent.cshtml");
        }

        [HttpGet]
        public IActionResult DetailEvent()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/DetailEvent.cshtml");
        }
        [HttpGet]
        public IActionResult Maps()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Maps.cshtml");
        }
        [HttpGet]
        public IActionResult About()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/About.cshtml");
        }
        [HttpGet]
        public IActionResult BlogEvent()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/BlogEvent.cshtml");
        }
        [HttpGet]
        public IActionResult DetailMap()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/DetailMap.cshtml");
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Privacy.cshtml");
        }
        [HttpGet]
        public IActionResult Terms()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Terms.cshtml");
        }
    }
}
