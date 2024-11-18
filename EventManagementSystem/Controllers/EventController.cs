using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class EventController : Controller
    {
        // GET
        public IActionResult AllEvent()
        {
            return View();
        }
    }
}