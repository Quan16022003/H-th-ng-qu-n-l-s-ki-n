using Constracts.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abtractions
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDTO>> GetAllTicketsAsync();

        Task<IEnumerable<TicketDTO>> GetAllTicketsByEventIdAsync(int id);
        Task<TicketDTO> GetTicketByIdAsync(int id);

        Task AddTicketAsync(TicketDTO ticketDTO);

        Task UpdateTicketAsync(TicketDTO ticketDTO);
      
        Task DeleteTicketAsync(int id);

    }
}
