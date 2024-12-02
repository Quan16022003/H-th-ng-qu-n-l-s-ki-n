using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Controllers;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageBookings
{
    [Authorize(Policy = "TicketManagement")]
    [Area("Dashboard")]
    public class TicketController : BaseController
    {
        public TicketController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<TicketController>();
        }

        public IActionResult Index()
        {
            return View($"{ViewPath}/Ticket.cshtml");
        }
    }
}
