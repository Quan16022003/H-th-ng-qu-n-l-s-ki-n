using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public PaymentController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        [HttpPost("CreatePaymentUrlVnpay")]
        public async Task<IActionResult> CreatePaymentUrlVnpay(PaymentInfomationDTO model)
        {
            var url = await _vnPayService.CreatePaymentUrlAsync(model, HttpContext);
            return Redirect(url);
        }

        
    }
}