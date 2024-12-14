using Constracts.DTO;
using Domain.Enum;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Services.Abtractions;
using System.Security.Claims;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IServiceManager _serviceManager;
        private readonly IVnPayService _vnPayService;
        public PaymentController(IServiceManager serviceManager, IVnPayService vnPayService)
        {
            _serviceManager = serviceManager;
            _vnPayService = vnPayService;
        }

        [HttpGet]
        [Route("event/{eventId:int}/booking/{orderId:int}/payment")]
        public async Task<IActionResult> Payment(int eventId, int orderId)
        {

            var order = await _serviceManager.OrdersService.GetOrderByIdAsync(orderId);
            if (order == null || order.EventId != eventId)
            {
                RedirectToAction("Index", "Home"); // Chuyển hướng về trang chủ
            }
            if (order?.OrderStatus != OrderStatus.Pending || order.CreatedDate < DateTime.Now.AddMinutes(-15))
            {
                // Đơn hàng đã được thanh toán hoặc quá thời gian 15 phút
                return RedirectToAction("Index", "Home"); // Chuyển hướng về trang chủ
            }

            var eventDetail = await _serviceManager.EventService.GetEventByIdAsync(eventId);
            if (eventDetail == null)
            {
                return NotFound();
            }

            var totalAmount = order.OrderItems.Sum(item => item.Quantity * item.UnitPrice); //Calculate total amount

            var selectedTickets = order.OrderItems.Select(item => new TicketTypeViewModel
            {
                Id = item.Id,
                Title = item.Title,
                QuantityBuy = item.Quantity,
                Price = item.UnitPrice
            }).ToList();


            var viewModel = new PaymentViewModel
            {
                EventId = eventDetail.Id,
                EventTitle = eventDetail.Title,
                EventDate = eventDetail.StartDate?.ToString("dd/MM/yyyy"),
                EventLocation = eventDetail.VenueName,
                TicketTypes = selectedTickets,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Email = order.Email,
                PhoneNumber = "", // Assuming Order has PhoneNumber property
                IsPhoneNumberEditable = true, // Assuming Order has this property
                IsNameEditable = true,        // Assuming Order has this property
                TotalAmount = totalAmount //Add total amount to viewmodel
            };

            ViewBag.OrderId = orderId;
            
            return View(viewModel);
        }

        [HttpPost]
        [Route("event/{eventId:int}/booking/{orderId:int}/payment")]
        public async Task<IActionResult> Payment(int eventId, int orderId, PaymentViewModel model)
        {
            var order = await _serviceManager.OrdersService.GetOrderByIdAsync(orderId);
            if (order == null || order?.EventId != eventId)
            {
                return BadRequest();
            }

            await _serviceManager.OrdersService.ConfirmOrderAsync(orderId);
            return RedirectToAction("Confirmation", new { orderId });

            // Gọi VNPAY (đang bị lỗi)
            /*var payInfo = new PaymentInfomationDTO
            {
                Amount = (double)model.TotalAmount,
                OrderId = orderId,
                OrderType = "BUY",
                Name = $"Đơn hàng {orderId} - {model.EventTitle}",
                OrderDescription = $"Thanh toán vé sự kiện {model.EventTitle} - Ngày {model.EventDate} tại {model.EventLocation}"
            };

            //Store orderId in session or TempData for later retrieval in callback
            TempData["OrderId"] = orderId;

            return RedirectToAction("CreatePaymentUrl", "Checkout", new { payInfo = payInfo });
*/
        }



        [HttpPost]
        public IActionResult CancelOrder([FromQuery]int orderId)
        {
            try
            {
                Console.WriteLine(orderId);
                _serviceManager.OrdersService.CancelledOrderAsync(orderId);
                return Ok(new { success = true });
            } catch (Exception ex)
            {
                // Log the exception properly
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }



        [HttpGet]
        [Route("order/{orderId:int}/confirmation")]
        public async Task<IActionResult> Confirmation(int orderId)
        {
            var order = await _serviceManager.OrdersService.GetOrderByIdAsync(orderId);
            var eventDetail = await _serviceManager.EventService.GetEventByIdAsync(order.EventId);


            var viewModel = new PaymentConfirmationViewModel
            {
                OrderId = orderId,
                EventTitle = eventDetail.Title,
                EventDate = eventDetail.StartDate ?? DateTime.MinValue,  //Handle potential null
                EventLocation = eventDetail.VenueName,
                TotalAmount = order.OrderItems.Sum(item => item.Quantity * item.UnitPrice),
                TicketTypes = order.OrderItems.Select(item => new TicketTypeViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    QuantityBuy = item.Quantity,
                    Price = item.UnitPrice
                }).ToList()

            };

            return View(viewModel);
        }
    }
}
