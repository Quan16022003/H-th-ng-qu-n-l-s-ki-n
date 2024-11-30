using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;

namespace Web.Controllers
{
    [ApiController]
    [Route("/api[controller")]
    public class OrderController : ControllerBase
    {   
        private IOrdersService _ordersService;
        public OrderController(IOrdersService ordersService) {
            _ordersService = ordersService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest("OrderDTO cannot be null.");
            }

            try
            {
                // Gọi service để tạo đơn hàng
                var orderId = await _ordersService.CreateOrderAsync(orderDTO);

                return Ok(new { OrderId = orderId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
