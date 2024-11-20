using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constracts.EventDto;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Services.Abtractions;
using Mapster;
using Domain.Exceptions;

namespace Services
{
    public sealed class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EventService> _logger;
        public EventService(IUnitOfWork unitOfWork, ILogger<EventService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task AddEventAsync(CreateEventDto createEventDto)
        {
            try
            {
                _logger.LogInformation("Creating new event: {@CreateEventDto}", createEventDto);
                var _event = createEventDto.Adapt<Events>();
                await _unitOfWork.EventRepository.AddAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event created successfully with id: {EventId}", _event.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating event: {@EventDto}", createEventDto);
                throw new EventCreationException(ex.Message, ex);
            }
        }

        public async Task DeleteEventAsync(int id)
        {
            try
            {
                _logger.LogInformation("Deleting event with id: {EventId}", id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(id);
                if (_event == null)
                {
                    _logger.LogWarning("Event with id: {EventId} was not found for deletion", id);
                    throw new EventNotFoundException(id);
                }

                await _unitOfWork.EventRepository.SoftDeleteAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event with id: {EventId} deleted successfully", id);
            }
            catch (Exception ex) when (ex is not EventNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while deleting event with id: {EventId}", id);
                throw new EventDeletionException(id, ex.Message, ex);
            }
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all events");
                var _events = await _unitOfWork.EventRepository.GetAllAsync();
                return _events.Adapt<IEnumerable<EventDto>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all events");
                throw;
            }
        }

        public async Task<IEnumerable<EventListDto>> GetAllWithCategoryAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all events and category");
                var _events = await _unitOfWork.EventRepository.GetAllWithCategoryAsync();
                var eventDtos = _events.Select(e => new EventListDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    CategoryEventName = e.CategoryEvent?.Name,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                    IsPublic = e.IsPublic,
                    ModifiedDate = e.ModifiedDate 
                });

                return eventDtos.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all events and category");
                throw;
            }
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

            try
            {
                _logger.LogInformation("Fetching event with id: {EventId}", id);

                var _event = await _unitOfWork.EventRepository.GetByIdAsync(id);

                if (_event is null)
                {
                    _logger.LogWarning("Event not found");
                    throw new EventNotFoundException(id);
                }

                return _event.Adapt<EventDto>();
            }
            catch (Exception ex) when (ex is not EventNotFoundException
                                           and not ArgumentOutOfRangeException)
            {
                _logger.LogError(ex, "Error occurred while fetching event with id: {EventId}", id);
                throw;
            }
        }

        public async Task UpdateEventDetailAsync(EventDetailDto eventDetailDto)
        {
            try
            {
                _logger.LogInformation("Updating event detail with id: {EventId}", eventDetailDto.Id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(eventDetailDto.Id);
                if (_event == null)
                {
                    _logger.LogWarning("Event detail with id: {EventId} was not found for update", eventDetailDto.Id);
                    throw new EventNotFoundException(eventDetailDto.Id);
                }

                eventDetailDto.Adapt(_event);
                await _unitOfWork.EventRepository.UpdateAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event detail with id: {EventId} updated successfully", eventDetailDto.Id);
            }
            catch (Exception ex) when (ex is not EventNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating event detail: {@EventDto}", eventDetailDto);
                throw new EventDetailUpdateException(eventDetailDto.Id, ex.Message, ex);
            }
        }

        public async Task UpdateEventMediaAsync(EventMediaDto eventMediaDto)
        {
            try
            {
                _logger.LogInformation("Updating event media with id: {EventId}", eventMediaDto.Id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(eventMediaDto.Id);
                if (_event == null)
                {
                    _logger.LogWarning("Event media with id: {EventId} was not found for update", eventMediaDto.Id);
                    throw new EventNotFoundException(eventMediaDto.Id);
                }

                eventMediaDto.Adapt(_event);
                await _unitOfWork.EventRepository.UpdateAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event media with id: {EventId} updated successfully", eventMediaDto.Id);
            }
            catch (Exception ex) when (ex is not EventNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating event media: {@EventDto}", eventMediaDto);
                throw new EventMediaUpdateException(eventMediaDto.Id, ex.Message, ex);
            }
        }

        public async Task UpdateEventTiminglAsync(EventTimingDto eventTimingDto)
        {
            try
            {
                _logger.LogInformation("Updating event timing with id: {EventId}", eventTimingDto.Id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(eventTimingDto.Id);
                if (_event == null)
                {
                    _logger.LogWarning("Event timing with id: {EventId} was not found for update", eventTimingDto.Id);
                    throw new EventNotFoundException(eventTimingDto.Id);
                }

                eventTimingDto.Adapt(_event);
                await _unitOfWork.EventRepository.UpdateAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event timing with id: {EventId} updated successfully", eventTimingDto.Id);
            }
            catch (Exception ex) when (ex is not EventNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating event timing: {@EventDto}", eventTimingDto);
                throw new EventTimingUpdateException(eventTimingDto.Id, ex.Message, ex);
            }
        }

        public async Task UpdateEventVenueAsync(EventVenueDto eventVenueDto)
        {
            try
            {
                _logger.LogInformation("Updating event venue with id: {EventId}", eventVenueDto.Id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(eventVenueDto.Id);
                if (_event == null)
                {
                    _logger.LogWarning("Event venue with id: {EventId} was not found for update", eventVenueDto.Id);
                    throw new EventNotFoundException(eventVenueDto.Id);
                }

                eventVenueDto.Adapt(_event);
                await _unitOfWork.EventRepository.UpdateAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event venue with id: {EventId} updated successfully", eventVenueDto.Id);
            }
            catch (Exception ex) when (ex is not EventNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating event venue: {@EventDto}", eventVenueDto);
                throw new EventVenueUpdateException(eventVenueDto.Id, ex.Message, ex);
            }
        }
    }
}
