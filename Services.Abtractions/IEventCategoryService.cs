using Constracts.EventCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abtractions
{
    public interface IEventCategoryService
    {
        Task<EventCategoryDto> GetByIdAsync(int id);
        Task<IEnumerable<EventCategoryDto>> GetAllAsync();
        Task<EventCategoryDto> CreateAsync(CreateEventCategoryDto createDto);
        Task<EventCategoryDto> UpdateAsync(int id, UpdateEventCategoryDto updateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<EventCategoryDto> GetBySlugAsync(string slug);
    }
}
