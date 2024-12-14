using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abtractions;

namespace Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly IServiceManager _serviceManager;

        public CheckoutController(IVnPayService vnPayService, IServiceManager serviceManager)
        {
            _vnPayService = vnPayService;
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> CreatePaymentUrl(PaymentInfomationDTO payInfo)
        {
            try
            {
                var url = await _vnPayService.CreatePaymentUrlAsync(payInfo, HttpContext);
                return Redirect(url);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi tạo liên kết thanh toán. Vui lòng thử lại sau.";
                return RedirectToAction("Error", "Home"); // Redirect to a proper error page
            }
        }

        [HttpGet]
        public async Task<IActionResult> PaymentCallbackVnpay()
        {
            //var response = await _vnPayService.PaymentExecuteAsync(Request.Query);
            //return new JsonResult(response);

            var response = await _vnPayService.PaymentExecuteAsync(Request.Query);

            if (response != null)
            {
                int orderId = (int)TempData["OrderId"]; //Retrieve orderId from TempData

                //After successful verification, update the order status
                await _serviceManager.OrdersService.ConfirmOrderAsync(orderId);

                return RedirectToAction("Confirmation", "Payment", new { orderId });
            }
            else
            {
                //Handle failure appropriately, potentially showing an error message
                TempData["Error"] = "Thanh toán không thành công. Vui lòng thử lại.";
                return RedirectToAction("Error", "Home"); //Redirect to a proper error page
            }
        }
    }
}
