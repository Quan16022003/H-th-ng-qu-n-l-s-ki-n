using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class AttendeeRepository(RepositoryDbContext dbContext)
     : BaseRepository<Attendees>(dbContext), IAttendeeRepository
    {
    }
}
