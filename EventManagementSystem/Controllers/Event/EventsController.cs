using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Utils.ViewsPathServices;


namespace Web.Controllers.Event
{
    public class EventsController : BaseController
    {
        public EventsController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<EventsController>();
        }

        [HttpGet]
        public IActionResult AllEvent()
        {
            return View($"{ViewPath}/AllEvent.cshtml");
        }

        [HttpGet]
        public IActionResult DetailEvent()
        {
            return View($"{ViewPath}/DetailEvent.cshtml");
        }
        [HttpGet]
        public IActionResult Maps()
        {
            return View($"{ViewPath}/Maps.cshtml");
        }
        [HttpGet]
        public IActionResult About()
        {
            return View($"{ViewPath}/About.cshtml");
        }
        [HttpGet]
        public IActionResult BlogEvent()
        {
            return View($"{ViewPath}/BlogEvent.cshtml");
        }
        [HttpGet]
        public IActionResult DetailMap()
        {
            return View($"{ViewPath}/DetailMap.cshtml");
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View($"{ViewPath}/Privacy.cshtml");
        }
        [HttpGet]
        public IActionResult Terms()
        {
            return View($"{ViewPath}/Terms.cshtml");
        }
    }
}
