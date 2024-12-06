using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private OrdersService _ordersService;
        public OrderController(OrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        // lấy danh sách đơn hàng
        [HttpGet]
        [Route("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _ordersService.GetOrdersAsync();
            return Ok(orders);
        }
        // lấy danh sách đơn hàng theo id event
        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetOrdersByEventId(int eventId)
        {
            var orders = await _ordersService.GetOrdersByEventIdAsync(eventId);
            if (!orders.Any())
            {
                return NotFound(new { Message = $"không tìm thấy đơn hàng có theo id này!!! " });              
            }
            return Ok(orders);
        }
        // lấy danh sách đơn hàng theo id event
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(String userId)
        {
            var orders = await _ordersService.GetOrdersByUserIdAsync(userId);
            if (!orders.Any())
            {
                return NotFound(new { Message = $"không tìm thấy đơn hàng theo id này!!! " });
            }
            return Ok(orders);
        }

    }
}
