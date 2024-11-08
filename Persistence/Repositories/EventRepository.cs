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
        }

        protected override async Task<IEnumerable<Events>> GetListWithInclude(IQueryable<Events> queryable)
        {
            return await queryable
                .Include(e => e.Organizer)
                .Include(e => e.CategoryEvent)
                .Include(e => e.Tickets)
                .Include(e => e.Orders)
                .Include(e => e.Attendees)
                .ToListAsync();
        }

        public async Task DeleteAsync(Events events)
        {
            events.IsDeleted = true;
            await UpdateAsync(events);
        }

    }
}
