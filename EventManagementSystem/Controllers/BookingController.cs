using Constracts.DTO;
using Domain.Enum;
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
        [Route("/event/{eventId}/booking/select-ticket")]
        public async Task<IActionResult> SelectTicket(int eventId)
        {
            var viewModel = new SelectTicketViewModel
            {
                eventDTO = await _serviceManager.EventService.GetEventByIdAsync(eventId),
                ticketDTOs = await _serviceManager.TicketService.GetAllTicketsByEventIdAsync(eventId)
            };
            return View(viewModel);
        }
        [HttpPost]
        [Route("/event/{eventId}/booking/select-ticket")]
        public async Task<IActionResult> SelectTicket(int eventId, [FromBody] Dictionary<int, int> selectedTickets)
        {
            try 
            {
                var orderDTO = new OrderDTO
                {
                    EventId = eventId,
                    OrderItems = new List<OrderItemDTO>(),
                };

                // Lấy thông tin vé từ service
                var tickets = await _serviceManager.TicketService.GetAllTicketsByEventIdAsync(eventId);
                
                // Tạo các order items từ vé được chọn
                foreach (var selection in selectedTickets)
                {
                    IEnumerable<TicketDTO> ticketDtos = tickets as TicketDTO[] ?? tickets.ToArray();
                    var ticket = ticketDtos.FirstOrDefault(t => t.Id == selection.Key);
                    if (ticket != null && selection.Value > 0)
                    {
                        orderDTO.OrderItems.Add(new OrderItemDTO
                        {
                            Title = ticket.Title,
                            Quantity = selection.Value,
                            UnitPrice = ticket.Price
                        });
                    }
                }

                
                return RedirectToAction("Checkout", new { id = eventId, eventId=1 });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo phù hợp
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpGet]
        [Route("/event/{id}/booking/{orderId}/checkout")]
        public IActionResult Checkout(int id, int orderId)
        {
            return View();
        }
    }
}