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
    public sealed class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository; // eventRepository có thể là EventRepository
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Events>> GetAllEventsAsync() => await _eventRepository.GetAllAsync();

        public async Task<Events> GetEventByIdAsync(int id) => await _eventRepository.GetByIdAsync(id);

        public async Task AddEventAsync(Events events)
        {
            await _eventRepository.AddAsync(events);
            _unitOfWork.CompleteAsync();
        }

        public async Task UpdateEventAsync(Events events)
        {
            await _eventRepository.UpdateAsync(events);
            _unitOfWork.CompleteAsync();
        }

        public async Task DeleteEventAsync(Events events)
        {
            await _eventRepository.DeleteAsync(events);
            _unitOfWork.CompleteAsync();
        }
    }
}
