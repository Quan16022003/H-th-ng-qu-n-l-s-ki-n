using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOrderRepository : IBaseRepository<Orders>
    {
        // Thêm các phương thức đặc thù cho Orders nếu cần
        // lấy danh sách đơn hàng theo id sự kiện
        Task<List<Orders>>GetOrdersByEventIdAsync(int eventId);
        Task<List<Orders>> GetOrdersByUserIdAsync(string userId);


        Task<IEnumerable<Orders>> GetOrdersReadyForCancellationAsync(CancellationToken cancellationToken);
        Task<Orders?> GetOrderByUserAndEventIdAsync(string userId, int eventId);
    }
} 