using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Services.Abtractions;

namespace Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository; // eventRepository có thể là EventRepository
        }

        public async Task<IEnumerable<Events>> GetAllEventsAsync() => await _eventRepository.GetAllAsync();

        public async Task<Events> GetEventByIdAsync(int id) => await _eventRepository.GetByIdAsync(id);

        public async Task AddEventAsync(Events events) => await _eventRepository.AddAsync(events);

        public async Task UpdateEventAsync(Events events) => await _eventRepository.UpdateAsync(events);

        public async Task DeleteEventAsync(Events events) => await _eventRepository.DeleteAsync(events);
    }
}
