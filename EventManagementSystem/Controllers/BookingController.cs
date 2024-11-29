using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.ViewModels.Booking;


namespace Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IServiceManager _serviceManager;
        
        public BookingController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Route("/event/{id}/booking/select-ticket")]
        public IActionResult SelectTicket(int id)
        {
            var viewModel = new SelectTicketViewModel();
            return View(viewModel);
        }
        [HttpPost]
        [Route("/event/{id}/booking/select-ticket")]
        public IActionResult SelectTicket(int id, SelectTicketViewModel viewModel)
        {
            int OrderId = 123;
            return RedirectToAction("Checkout", new { id, OrderId });
        }
        
        [HttpGet]
        [Route("/event/{id}/booking/{orderId}/checkout")]
        public IActionResult Checkout(int id, int orderId)
        {
            return View();
        }
    }
}