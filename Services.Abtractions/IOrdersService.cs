using Constracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abtractions
{
    public  interface IOrdersService
    {
        Task<int> CreateOrderAsync(OrderDTO orderDTO);
        Task<List <OrderDTO>> GetOrdersAsync();
        Task<List<OrderDTO>> GetOrdersByEventIdAsync(int eventId);
        Task<List<OrderDTO>> GetOrdersByUserIdAsync(string userId);
        Task<bool> ConfirmOrderAsync(int orderId);
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
        Task<bool> UpdateOrderAsync(OrderDTO order);
        Task<int> CancelledOrderAsync(int orderId);
        Task<int?> GetPendingEventOrderId(string userId, int eventId);
    }
}
