using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public sealed class TicketRepository(RepositoryDbContext dbContext)
     : BaseRepository<Tickets>(dbContext), ITicketRepository
    {
        public async Task UpdateTicketQuantitySold(int ticketId, int quantitySold)
        {
            var ticket = await _dbSet.FirstOrDefaultAsync(t => t.Id == ticketId);
            Console.WriteLine("Quantity sold to update: " + quantitySold);
            ticket.QuantitySold += quantitySold;
            Console.WriteLine(ticket.Title + " has quantity sold: " + ticket.QuantitySold);
            await _dbContext.SaveChangesAsync();
        }
    }
}
