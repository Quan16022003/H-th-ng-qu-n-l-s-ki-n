using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Repositories
{
    public sealed class EventRepository : BaseRepository<Events>, IEventRepository
    {
        public EventRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            References.AddMultiple([
                e => e.Organizer!,
                e => e.CategoryEvent!,
                e => e.Tickets!,
                e => e.Orders!,
                e => e.Attendees!
            ]);
        }
        public Task<IEnumerable<Events>> GetAllWithCategoryAsync()
        {
            return GetManyAsync(includeProperties: nameof(Events.CategoryEvent));
        }

    }
}
