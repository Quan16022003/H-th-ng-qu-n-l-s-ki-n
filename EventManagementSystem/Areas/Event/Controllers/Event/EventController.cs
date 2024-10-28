using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Event.Controllers.Event
{
    [Area("Event")]
    public class EventController : Controller
    {
        [HttpGet]
        public IActionResult AllEvent()
        {
            return View();
        }
    }
}
