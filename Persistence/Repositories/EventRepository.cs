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
        public async Task DeleteAsync(Events events)
        {
            events.IsDeleted = true;
            await UpdateAsync(events);
        }

    }
}
