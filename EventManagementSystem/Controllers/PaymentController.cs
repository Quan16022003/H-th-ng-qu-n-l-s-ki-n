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
        public PaymentController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
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
                Console.WriteLine("Chuyển hướng 2");
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

            model.Adapt(order);
            await _serviceManager.OrdersService.UpdateOrderAsync(order);

            if (PaymentProcess(orderId).Result)
            {
                return RedirectToAction("Confirmation", new { orderId });
            }
            return BadRequest();
            //return Redirect($"/order/{orderId}/success");
            return RedirectToAction("Confirmation", new { orderId });
        }

        public async Task<IActionResult> PaymentConfirm(int orderId)
        {
            try
            {
                await _serviceManager.OrdersService.ConfirmOrderAsync(orderId);

                return RedirectToAction("PaymentSuccess", new {orderId});
            } catch (Exception ex)
            {
                return NotFound();
            }
        }

        public async Task<bool> PaymentProcess( int orderId)
        {
            // Thực hiện các thao tác với vnpay

            try
            {
                //Simulate Payment Gateway interaction
                var paymentResult = await SimulatePaymentGateway(orderId);
                if (!paymentResult)
                {
                    return false;
                }
                await _serviceManager.OrdersService.ConfirmOrderAsync(orderId);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception properly
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private async Task<bool> SimulatePaymentGateway(int orderId){
            //Replace this with your actual Payment Gateway integration
            await Task.Delay(1000); //Simulate processing time
            return true; //Simulate success
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
