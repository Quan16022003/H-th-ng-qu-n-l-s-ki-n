using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Constracts.DTO;

namespace Services.Abtractions
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAllEventsAsync();

        Task<EventDTO> GetEventByIdAsync(int id);

        Task AddEventAsync(EventDTO events);

        Task UpdateEventAsync(EventDTO events);

        Task DeleteEventAsyncById(int id);

        Task DeleteEventAsync(EventDTO events);

    }
}
