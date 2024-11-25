using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Controllers;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageBookings
{
    [Authorize(Policy = "OrderManagement")]
    [Area("Dashboard")]
    public class BookingController : BaseController
    {
        public BookingController(
            IPathProvideManager pathProviderManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProviderManager.Get<BookingController>();
        }

        public IActionResult Index()
        {
            return View($"{ViewPath}/Bookings.cshtml");
        }
    }
}
