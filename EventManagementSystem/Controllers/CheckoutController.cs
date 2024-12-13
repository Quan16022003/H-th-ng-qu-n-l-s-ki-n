using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController :ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public CheckoutController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }
        [HttpGet("PaymentCallbackVnpay")]
        public async Task<IActionResult> PaymentCallbackVnpay()
        {
            var response = await _vnPayService.PaymentExecuteAsync(Request.Query);
            return new JsonResult(response);
        }
    }
}
