using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;


namespace Web.Controllers.Event
{
    public class EventsController : Controller
    {
        private readonly string viewPath;

        public EventsController(IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<EventsController>();
        }

        [HttpGet]
        public IActionResult AllEvent()
        {
            return View($"{viewPath}/AllEvent.cshtml");
        }

        [HttpGet]
        public IActionResult DetailEvent()
        {
            return View($"{viewPath}/DetailEvent.cshtml");
        }
        [HttpGet]
        public IActionResult Maps()
        {
            return View($"{viewPath}/Maps.cshtml");
        }
        [HttpGet]
        public IActionResult About()
        {
            return View($"{viewPath}/About.cshtml");
        }
        [HttpGet]
        public IActionResult BlogEvent()
        {
            return View($"{viewPath}/BlogEvent.cshtml");
        }
        [HttpGet]
        public IActionResult DetailMap()
        {
            return View($"{viewPath}/DetailMap.cshtml");
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View($"{viewPath}/Privacy.cshtml");
        }
        [HttpGet]
        public IActionResult Terms()
        {
            return View($"{viewPath}/Terms.cshtml");
        }
    }
}
