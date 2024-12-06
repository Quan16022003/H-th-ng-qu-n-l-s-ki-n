using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public sealed class OrderRepository : BaseRepository<Orders>, IOrderRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public OrderRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Orders>> GetOrdersByEventIdAsync(int eventId)
        {
            return await _dbContext.Set<Orders>()
                .Include(o => o.OrderItems) // Nạp thêm thông tin OrderItems
                .Where(o => o.EventId == eventId && !o.IsDeleted) // Điều kiện lọc
                .ToListAsync();
        }
        public async Task<List<Orders>> GetOrdersByUserIdAsync(string userId)
        {
            return await _dbContext.Set<Orders>()
                .Include(o => o.OrderItems) // Nạp thêm thông tin OrderItems
                .Where(o => o.UserId == userId && !o.IsDeleted) // Điều kiện lọc theo UserId và chưa bị xóa
                .ToListAsync();
        }

    }
}
