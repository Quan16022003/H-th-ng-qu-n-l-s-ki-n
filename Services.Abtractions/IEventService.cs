using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Constracts.DTO;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Services.Abtractions
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAllEventsAsync();
        Task<IEnumerable<EventDTO>> GetAllEventsComingAsync();
        Task<IEnumerable<EventDTO>> GetAllEventsBestSellingAsync();
        Task<IEnumerable<EventDTO>> GetAllEventsOutstandingAsync();
        Task<EventDTO> GetEventByIdAsync(int id);

        Task AddEventAsync(EventDetailDTO eventDetailDTO);

        Task UpdateEventDetailAsync(EventDetailDTO eventDetailDTO);
        Task UpdateEventTiminglAsync(EventTimingDTO eventTimingDTO);
        Task UpdateEventMediaAsync(EventMediaDTO eventMediaDTO, IFormFile? thumbnailFile, IFormFile? coverFile);
        Task UpdateEventVenueAsync(EventVenueDTO eventVenueDTO);
        Task DeleteEventAsync(int id);

    }
}
