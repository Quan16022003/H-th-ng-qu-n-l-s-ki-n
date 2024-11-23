using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class OrderRepository(RepositoryDbContext dbContext)
     : BaseRepository<Orders>(dbContext), IOrderRepository
    {
    }
}
