using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class OrderTicketRepository(RepositoryDbContext dbContext)
     : BaseRepository<OrderTicket>(dbContext), IOrderTicketRepository
    {
    }
}
