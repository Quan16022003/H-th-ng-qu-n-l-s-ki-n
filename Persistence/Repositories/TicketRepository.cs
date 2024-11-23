using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class TicketRepository(RepositoryDbContext dbContext)
     : BaseRepository<Tickets>(dbContext), ITicketRepository
    {
    }
}
