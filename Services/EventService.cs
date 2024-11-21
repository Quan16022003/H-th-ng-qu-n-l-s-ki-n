using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constracts.DTO;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using Microsoft.Extensions.Logging;
using Services.Abtractions;

namespace Services
{
    internal sealed class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventRepository = unitOfWork.EventRepository; // eventRepository có thể là EventRepository
        }

        public async Task<IEnumerable<EventDTO>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            return events.Adapt<IEnumerable<EventDTO>>();
        }

        public async Task<EventDTO> GetEventByIdAsync(int id)
        {
            var model = await _eventRepository.GetByIdAsync(id);
            return model.Adapt<EventDTO>();
        }

        public async Task AddEventAsync(EventDTO eventDTO)
        {
            ArgumentNullException.ThrowIfNull(eventDTO);
            var model = eventDTO.Adapt<Events>();

            await _eventRepository.AddAsync(model);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateEventAsync(EventDTO eventDTO)
        {
            ArgumentNullException.ThrowIfNull(eventDTO);
            var model = eventDTO.Adapt<Events>();

            await _eventRepository.UpdateAsync(model);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteEventAsyncById(int id)
        {
            var model = await _eventRepository.GetByIdAsync(id);
            ArgumentNullException.ThrowIfNull(model);

            await _eventRepository.SoftDeleteAsync(model);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteEventAsync(EventDTO eventDTO)
        {
            ArgumentNullException.ThrowIfNull(eventDTO);
            var model = eventDTO.Adapt<Events>();

            await _eventRepository.DeleteAsync(model);
            await _unitOfWork.CompleteAsync();
        }
    }
}
