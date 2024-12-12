using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Booking;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using Services.Abtractions;
using System.Collections.Generic;
using System.Text.Json;

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
        [Route("/event/{id:int}/booking/select-ticket")]
        public IActionResult SelectTicket(int id)
        {
            SelectTicketViewModel viewModel = new();
            try
            {
                EventDTO eventDetail = _serviceManager.EventService.GetEventByIdAsync(id).Result;
                viewModel.EventId = eventDetail.Id;
                viewModel.EventTitle = eventDetail.Title;
                viewModel.EventDate = eventDetail.StartDate?.ToString("dd/MM/yyyy");
                viewModel.EventLocation = eventDetail.VenueName;
                viewModel.EventImage = eventDetail.ThumbnailUrl;
                IEnumerable<TicketDTO>? tickets = _serviceManager.TicketService.GetAllTicketsByEventIdAsync(id).Result;
                
                viewModel.TicketTypes = tickets.Adapt<List<TicketTypeViewModel>>();

                return View(viewModel);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("/event/{id:int}/booking/select-ticket")]
        [IgnoreAntiforgeryToken]
        public IActionResult SelectTicket(int id, [FromForm] SelectTicketViewModel? viewModel)
        {
            if (viewModel?.TicketTypes == null)
            {
                return BadRequest("Invalid request");
            }
            

            // Validate data: check số lượng vé mua có hợp lệ không,...
            foreach (var ticket in viewModel.TicketTypes)
            {
                if (ticket.QuantityBuy < 0)
                {
                    ModelState.AddModelError($"TicketTypes[{viewModel.TicketTypes.IndexOf(ticket)}].QuantityBuy", "Số lượng vé không hợp lệ");
                }
                var currentTicket = _serviceManager.TicketService.GetTicketByIdAsync(ticket.Id).Result;
                if (currentTicket != null && ticket.QuantityBuy > currentTicket.QuantityAvailable - currentTicket.QuantitySold)
                {
                    ModelState.AddModelError($"TicketTypes[{viewModel.TicketTypes.IndexOf(ticket)}].QuantityBuy", "Số lượng vé vượt quá số vé còn lại");
                }
            }
            if (!ModelState.IsValid)
            {
                return View("SelectTicket", viewModel);
            }


            return RedirectToAction("Checkout", new { id });
        }

        public IActionResult Checkout(int id)
        {
            return View();
        }
    }
}