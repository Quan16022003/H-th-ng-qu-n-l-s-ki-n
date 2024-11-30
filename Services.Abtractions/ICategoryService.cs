using Constracts.EventCategory;
using Domain.ValueObjects;
namespace Services.Abtractions
{
    using EventCategoryResult = Result<EventCategoryDTO>;
    using EventCategoryListResult = Result<IEnumerable<EventCategoryDTO>>;

    public interface ICategoryService
    {
        Task<EventCategoryResult> GetByIdAsync(int id);
        Task<EventCategoryListResult> GetAllAsync(string type = "", string query = "");
        Task<Result<int>> CreateAsync(EventCategoryCreationDto createDto);
        Task<Result<int>> UpdateAsync(EventCategoryUpdateDto updateDto);
        Task<Result> DeleteAsync(int id);
        Task<EventCategoryResult> GetBySlugAsync(string slug);
    }
}
