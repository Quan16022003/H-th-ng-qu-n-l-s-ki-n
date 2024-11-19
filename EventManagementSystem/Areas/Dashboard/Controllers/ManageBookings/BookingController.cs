using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageBookings
{
    [Area("Dashboard")]
    public class BookingController : Controller
    {
        private readonly string viewPath;

        public BookingController(IPathProvideManager pathProviderManager)
        {
            viewPath = pathProviderManager.Get<BookingController>("Dashboard");
        }

        public IActionResult Index()
        {
            return View($"{viewPath}/Bookings.cshtml");
        }
    }
}
