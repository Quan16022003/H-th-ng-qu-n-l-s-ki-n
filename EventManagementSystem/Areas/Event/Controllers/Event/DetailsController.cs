using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Event.Controllers.Event
{
    [Area("Event")]
    public class DetailsController : Controller
    {
        [HttpGet]
        public IActionResult DetailEvent()
        {
            return View();
        }
    }
}
