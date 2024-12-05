using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constracts.DTO;
using Constracts.Home;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Services.Abtractions;
using Domain.Exceptions;
using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Services
{
    internal sealed class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EventService> _logger;
        private readonly IFileService _fileService;
        public EventService(IUnitOfWork unitOfWork, ILogger<EventService> logger, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _fileService = fileService;
        }

        public async Task AddEventAsync(EventDetailDTO eventDetailDTO)
        {
            try
            {
                _logger.LogInformation("Creating new event: {@CreateEventDTO}", eventDetailDTO);
                var _event = eventDetailDTO.Adapt<Events>();
                await _unitOfWork.EventRepository.AddAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event created successfully with id: {EventId}", _event.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating event: {@EventDTO}", eventDetailDTO);
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

        public async Task<IEnumerable<EventDTO>> GetAllEventsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all events");
                var _events = await _unitOfWork.EventRepository.GetAllAsync();

                _events = _events.Where(c => c.IsDeleted == false).ToList();

                return _events.Adapt<IEnumerable<EventDTO>>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all events");
                throw;
            }
        }
        // sự kiện sắp tới
        public async Task<IEnumerable<HomeEventDTO>> GetAllEventsComingAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all upcoming events");

                var upcomingEvents = await _unitOfWork.EventRepository.GetManyAsync(
                    filter: e => !e.IsDeleted && e.StartDate.HasValue && e.StartDate.Value > DateTime.Now,
                    includeProperties: new[] { "CategoryEvent", "Tickets" },
                    orderBy: q => q.OrderBy(e => e.StartDate)
                );

                return upcomingEvents.Select(e => new HomeEventDTO(e)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all upcoming events");
                throw;
            }
        }
        // sự kiện bán chạy
        public async Task<IEnumerable<HomeEventDTO>> GetAllEventsBestSellingAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all events best selling");
                var activeEvents = await _unitOfWork.EventRepository.GetManyAsync(
                    filter: e => !e.IsDeleted,
                    includeProperties: new[] { "CategoryEvent", "Tickets" },
                    orderBy: q => q.OrderBy(e => e.StartDate)
                );

                var currentDate = DateTime.Now;

                var bestSellingEvents = activeEvents
                    .Select(e => new
                    {
                        Event = e,
                        TotalSold = e.Tickets?.Sum(t => t.QuantitySold) ?? 0, 
                        TotalAvailable = e.Tickets?.Sum(t => t.QuantityAvailable) ?? 0 
                    })
                    .Where(x => x.TotalAvailable > 0 &&
                                x.Event.StartDate.HasValue &&
                                x.Event.StartDate.Value > currentDate)
                    .Select(x => new
                    {
                        x.Event,
                        SalesRatio = (double)x.TotalSold / x.TotalAvailable
                    })
                    .OrderByDescending(x => x.SalesRatio)
                    .Select(x => x.Event)
                    .ToList();

                return bestSellingEvents.Select(e => new HomeEventDTO(e)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all events best selling");
                throw;
            }
        }
        // sự kiện nổi bật điều kiện public + Bán chạy(tỉ lệ vé bán/tổng vé >=0,75) + diễn ra trong 1 tháng tới.
        public async Task<IEnumerable<HomeEventDTO>> GetAllEventsOutstandingAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all outstanding events");

                var currentDate = DateTime.Now;
                var nextMonthDate = currentDate.AddMonths(1);

                var activeEvents = await _unitOfWork.EventRepository.GetManyAsync(
                    filter: e => !e.IsDeleted && e.IsPublic && e.StartDate.HasValue && e.StartDate.Value > currentDate && e.StartDate.Value <= nextMonthDate,
                    includeProperties: new[] { "CategoryEvent", "Tickets" },
                    orderBy: q => q.OrderBy(e => e.StartDate)
                );

                var outstandingEvents = activeEvents
                    .Select(e => new
                    {
                        Event = e,
                        TotalSold = e.Tickets?.Sum(t => t.QuantitySold) ?? 0,
                        TotalAvailable = e.Tickets?.Sum(t => t.QuantityAvailable) ?? 0
                    })
                    .Where(x => x.TotalAvailable > 0 && (double)x.TotalSold / x.TotalAvailable >= 0.75)
                    .OrderBy(x => x.Event.StartDate)
                    .Select(x => x.Event)
                    .ToList();

                return outstandingEvents.Select(e => new HomeEventDTO(e)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all outstanding events");
                throw;
            }
        }

        public async Task<EventDTO> GetEventByIdAsync(int id)
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

                return _event.Adapt<EventDTO>();
            }
            catch (Exception ex) when (ex is not EventNotFoundException
                                           and not ArgumentOutOfRangeException)
            {
                _logger.LogError(ex, "Error occurred while fetching event with id: {EventId}", id);
                throw;
            }
        }

        public async Task UpdateEventDetailAsync(EventDetailDTO eventDetailDTO)
        {
            try
            {
                _logger.LogInformation("Updating event detail with id: {EventId}", eventDetailDTO.Id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(eventDetailDTO.Id);
                if (_event == null)
                {
                    _logger.LogWarning("Event detail with id: {EventId} was not found for update", eventDetailDTO.Id);
                    throw new EventNotFoundException(eventDetailDTO.Id);
                }

                eventDetailDTO.Adapt(_event);
                await _unitOfWork.EventRepository.UpdateAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event detail with id: {EventId} updated successfully", eventDetailDTO.Id);
            }
            catch (Exception ex) when (ex is not EventNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating event detail: {@EventDTO}", eventDetailDTO);
                throw new EventDetailUpdateException(eventDetailDTO.Id, ex.Message, ex);
            }
        }

        public async Task UpdateEventMediaAsync(EventMediaDTO eventMediaDTO, IFormFile? thumbnailFile, IFormFile? coverFile)
        {
            try
            {
                _logger.LogInformation("Updating event media with id: {EventId}", eventMediaDTO.Id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(eventMediaDTO.Id);
                if (_event == null)
                {
                    _logger.LogWarning("Event media with id: {EventId} was not found for update", eventMediaDTO.Id);
                    throw new EventNotFoundException(eventMediaDTO.Id);
                }
       
                if (thumbnailFile != null)
                {
                    var thumbnailUrl = await _fileService.UploadFileAsync(thumbnailFile, "images\\events\\thumbnails");
                    _event.ThumbnailUrl = thumbnailUrl;
                }

                if (coverFile != null)
                {
                    var coverUrl = await _fileService.UploadFileAsync(coverFile, "images\\events\\covers");
                    _event.CoverUrl = coverUrl;
                }

                eventMediaDTO.Adapt(_event);
                await _unitOfWork.EventRepository.UpdateAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event media with id: {EventId} updated successfully", eventMediaDTO.Id);
            }
            catch (Exception ex) when (ex is not EventNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating event media: {@EventDTO}", eventMediaDTO);
                throw new EventMediaUpdateException(eventMediaDTO.Id, ex.Message, ex);
            }
        }

        public async Task UpdateEventTiminglAsync(EventTimingDTO eventTimingDTO)
        {
            try
            {
                _logger.LogInformation("Updating event timing with id: {EventId}", eventTimingDTO.Id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(eventTimingDTO.Id);
                if (_event == null)
                {
                    _logger.LogWarning("Event timing with id: {EventId} was not found for update", eventTimingDTO.Id);
                    throw new EventNotFoundException(eventTimingDTO.Id);
                }

                eventTimingDTO.Adapt(_event);
                await _unitOfWork.EventRepository.UpdateAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event timing with id: {EventId} updated successfully", eventTimingDTO.Id);
            }
            catch (Exception ex) when (ex is not EventNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating event timing: {@EventDTO}", eventTimingDTO);
                throw new EventTimingUpdateException(eventTimingDTO.Id, ex.Message, ex);
            }
        }

        public async Task UpdateEventVenueAsync(EventVenueDTO eventVenueDTO)
        {
            try
            {
                _logger.LogInformation("Updating event venue with id: {EventId}", eventVenueDTO.Id);
                var _event = await _unitOfWork.EventRepository.GetByIdAsync(eventVenueDTO.Id);
                if (_event == null)
                {
                    _logger.LogWarning("Event venue with id: {EventId} was not found for update", eventVenueDTO.Id);
                    throw new EventNotFoundException(eventVenueDTO.Id);
                }

               
                var placeDetails = await GetPlaceDetailsAsync(eventVenueDTO.PlaceId);

                
                _event.PostalCode = placeDetails.PostalCode;
                _event.Latitude = placeDetails.Latitude;
                _event.Longitude = placeDetails.Longitude;
                _event.PlaceId = placeDetails.PlaceId;
                _event.PhoneNumber = placeDetails.PhoneNumber;
                _event.WebsiteUrl = placeDetails.WebsiteUrl;

                await _unitOfWork.EventRepository.UpdateAsync(_event);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Event venue with id: {EventId} updated successfully", eventVenueDTO.Id);
            }
            catch (Exception ex) when (ex is not EventNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while updating event venue: {@EventDTO}", eventVenueDTO);
                throw new EventVenueUpdateException(eventVenueDTO.Id, ex.Message, ex);
            }
        }
        public async Task<PlaceDetailsDTO> GetPlaceDetailsAsync(string placeId)
        {
            var apiKey = "YOUR_API_KEY"; // Thay thế bằng API key đúng
            var url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&key={apiKey}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(url);
                var placeDetails = JsonDocument.Parse(response).RootElement;

            
                if (placeDetails.GetProperty("status").GetString() != "OK")
                {
                    throw new Exception($"Lỗi khi lấy thông tin địa điểm: {placeDetails.GetProperty("status").GetString()}");
                }

                var result = placeDetails.GetProperty("result");
       
                return new PlaceDetailsDTO
                {
                    PostalCode = GetPostalCode(result),
                    Latitude = result.GetProperty("geometry").GetProperty("location").GetProperty("lat").GetDecimal(),
                    Longitude = result.GetProperty("geometry").GetProperty("location").GetProperty("lng").GetDecimal(),
                    PlaceId = result.GetProperty("place_id").GetString(),
                    PhoneNumber = result.TryGetProperty("formatted_phone_number", out var phone) ? phone.GetString() : null,
                    WebsiteUrl = result.TryGetProperty("website", out var website) ? website.GetString() : null
                };
            }
        }
        private string GetPostalCode(JsonElement result)
        {
            if (result.TryGetProperty("address_components", out var components) && components.ValueKind == JsonValueKind.Array)
            {
                foreach (var component in components.EnumerateArray())
                {
                    if (component.TryGetProperty("types", out var types) && types.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var type in types.EnumerateArray())
                        {
                            if (type.GetString() == "postal_code" && component.TryGetProperty("long_name", out var longName))
                            {
                                return longName.GetString();
                            }
                        }
                    }
                }
            }
            return null; 
        }
        public async Task<IEnumerable<HomeEventDTO>> GetAllEventsSelectedAsync(string? query, int? categoryId, string? city, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                _logger.LogInformation("Fetching all selected events with query: {Query}, categoryId: {CategoryId}, city: {City}, startDate: {StartDate}, endDate: {EndDate}", query, categoryId, city, startDate, endDate);


                var eventsQuery = await _unitOfWork.EventRepository.GetManyAsync(
                    filter: e =>
                        !e.IsDeleted &&
                        (string.IsNullOrEmpty(query) || e.Title.Contains(query)) &&
                        (!categoryId.HasValue || e.CategoryId == categoryId) &&
                        (string.IsNullOrEmpty(city) || e.City == city) &&
                        (!startDate.HasValue || !endDate.HasValue || (e.StartDate >= startDate && e.StartDate <= endDate)),
                    includeProperties: new[] { "CategoryEvent", "Tickets" },
                    orderBy: q => q.OrderBy(e => e.StartDate)
                );

                return eventsQuery.Select(e => new HomeEventDTO(e)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching selected events");
                throw;
            }
        }

        public async Task<EventDTO> GetEventBySlugAsync(string slug)
        {
            try
            {
                _logger.LogInformation("Fetching event with slug: {Slug}", slug);

                var _event = await _unitOfWork.EventRepository.GetBySlugAsync(slug);
                if (_event == null)
                {
                    _logger.LogWarning("Event with slug: {Slug} not found", slug);
                    throw new EventBySlugNotFoundException(slug);
                }

                return _event.Adapt<EventDTO>();
            }
            catch (Exception ex) when (ex is not EventBySlugNotFoundException)
            {
                _logger.LogError(ex, "Error occurred while fetching event with slug: {Slug}", slug);
                throw;
            }
        }
    }
}
