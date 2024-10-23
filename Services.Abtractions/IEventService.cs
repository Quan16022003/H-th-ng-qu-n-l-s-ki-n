using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace Services.Abtractions
{
    public interface IEventService
    {

        Task<IEnumerable<Events>> GetAllEventsAsync();

        Task<Events> GetEventByIdAsync(int id);

        Task AddEventAsync(Events events);



        Task UpdateEventAsync(Events events);


        Task DeleteEventAsync(Events events);

    }
}
