using Constracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abtractions
{
    public interface IEventCategoryService
    {
        Task<EventCategoryDTO> GetByIdAsync(int id);
        Task<IEnumerable<EventCategoryDTO>> GetAllAsync();
        Task<EventCategoryDTO> CreateAsync(EventCategoryDTO createDto);
        Task<EventCategoryDTO> UpdateAsync(int id, EventCategoryDTO updateDto);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<EventCategoryDTO> GetBySlugAsync(string slug);
    }
}
