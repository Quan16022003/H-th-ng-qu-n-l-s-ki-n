using Microsoft.AspNetCore.Mvc;
using Web.Utils;

namespace Web.Areas.Admin.Controllers.ManageBookings
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly string view = PathProvider.GetAdminManageBookings();
        public IActionResult Index()
        {
            return View($"{view}/Bookings.cshtml");
        }
    }
}
