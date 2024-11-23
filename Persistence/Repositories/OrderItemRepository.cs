using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class OrderItemRepository(RepositoryDbContext dbContext)
     : BaseRepository<OrderItems>(dbContext), IOrderItemRepository
    {
    }
}
