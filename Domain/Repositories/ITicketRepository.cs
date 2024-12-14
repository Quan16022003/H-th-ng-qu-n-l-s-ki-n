using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITicketRepository : IBaseRepository<Tickets>
    {
        // Thêm các phương thức đặc thù cho Tickets nếu cần
        Task UpdateTicketQuantitySold(int ticketId, int quantitySold);
    }
} 