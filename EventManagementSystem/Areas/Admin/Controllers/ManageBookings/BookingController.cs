using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Admin.Controllers.ManageBookings
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly IPathProvider _pathProvider;

        public BookingController([FromKeyedServices("Admin")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public IActionResult Index()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Bookings.cshtml");
        }
    }
}
