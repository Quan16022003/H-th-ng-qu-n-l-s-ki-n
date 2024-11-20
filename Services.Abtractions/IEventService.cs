using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;
using Constracts.EventDto;

namespace Services.Abtractions
{
    public interface IEventService
    {

        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<IEnumerable<EventListDto>> GetAllWithCategoryAsync();
        Task<EventDto> GetEventByIdAsync(int id);

        Task AddEventAsync(CreateEventDto createEventDto);

        Task UpdateEventDetailAsync(EventDetailDto eventDetailDto);
        Task UpdateEventTiminglAsync(EventTimingDto eventTimingDto);
        Task UpdateEventMediaAsync(EventMediaDto eventMediaDto);
        Task UpdateEventVenueAsync(EventVenueDto eventVenueDto);
        Task DeleteEventAsync(int id);

    }
}
