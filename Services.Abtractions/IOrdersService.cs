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
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
        Task<List<OrderDTO>> GetOrdersAsync();
        Task<List<OrderDTO>> GetOrdersByUserIdAsync(string userId);
        Task<List<OrderDTO>> GetOrdersByEventIdAsync(int eventId);
        Task<bool> UpdateOrderAsync(OrderDTO order);
        Task<bool> ConfirmOrderAsync(int orderId);
        Task<int> CancelledOrderAsync(int orderId);
        Task<int?> GetPendingEventOrderId(string userId, int eventId);
    }
}
