using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using Services.Abtractions;
using System.Collections.Generic;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookingController(IServiceManager serviceManager,
            UserManager<ApplicationUser> userManager)
        {
            _serviceManager = serviceManager;
            _userManager = userManager;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                string returnUrl = context.HttpContext.Request.Path; // Lấy đường dẫn hiện tại

                // Nếu cần thêm query string vào returnUrl
                if (context.HttpContext.Request.QueryString.HasValue)
                {
                    returnUrl += context.HttpContext.Request.QueryString.Value;
                }

                context.Result = new RedirectToActionResult("Login", "Account", new { ReturnUrl = returnUrl });
            }

            base.OnActionExecuting(context);
        }

        [HttpGet]
        [Route("/event/{eventId:int}/booking/select-ticket")]
        public async Task<IActionResult> SelectTicketAsync(int eventId)
        {
            var user = await _userManager.GetUserAsync(User);
            
            var orderId = await _serviceManager.OrdersService.GetPendingEventOrderId(user!.Id, eventId);
            if (orderId != null)
            {
                return RedirectToAction("Payment", "Payment", new { eventId, orderId });
            }

            SelectTicketViewModel viewModel = new();
            try
            {
                EventDTO eventDetail = _serviceManager.EventService.GetEventByIdAsync(eventId).Result;
                viewModel.EventId = eventDetail.Id;
                viewModel.EventTitle = eventDetail.Title;
                viewModel.EventDate = eventDetail.StartDate?.ToString("dd/MM/yyyy");
                viewModel.EventLocation = eventDetail.VenueName;
                viewModel.EventImage = eventDetail.ThumbnailUrl;
                IEnumerable<TicketDTO>? tickets = _serviceManager.TicketService.GetAllTicketsByEventIdAsync(eventId).Result;
                
                viewModel.TicketTypes = tickets.Adapt<List<TicketTypeViewModel>>();

                return View(viewModel);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [Route("/event/{eventId:int}/booking/select-ticket")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> CreateOrderAndRedirectAsync(int eventId, [FromForm] SelectTicketViewModel? viewModel)
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

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                OrderDTO orderDTO = new()
                {
                    FirstName = currentUser?.FirstName!,
                    LastName = currentUser?.LastName!,
                    Email = currentUser?.Email!,
                    UserId = currentUser?.Id!,
                    EventId = eventId,
                };
                orderDTO.OrderItems = new List<OrderItemDTO>();
                var ticketsByEvent = await _serviceManager.TicketService.GetAllTicketsByEventIdAsync(eventId);
                foreach (var ticketType in viewModel.TicketTypes)
                {
                    if (ticketType.QuantityBuy > 0)
                    {
                        var matchingTicket = ticketsByEvent.FirstOrDefault(ticket => ticket.Id == ticketType.Id)
                                             ?? throw new Exception("Mã ticket id không hợp lệ");
                        orderDTO.OrderItems.Add(new OrderItemDTO()
                        {
                            TicketId = ticketType.Id,
                            Quantity = ticketType.QuantityBuy,
                            Title = matchingTicket.Title!,
                            UnitPrice = matchingTicket.Price
                        });
                    }
                }

                // Gọi Service
                int orderId = await _serviceManager.OrdersService.CreateOrderAsync(orderDTO);

                return RedirectToAction("Payment", "Payment", new { eventId, orderId});
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message + viewModel.ToJson());
            }
        }

        public IActionResult Checkout(int id)
        {
            return View();
        }
    }
}