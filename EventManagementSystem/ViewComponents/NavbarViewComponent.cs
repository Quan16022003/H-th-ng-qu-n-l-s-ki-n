using Constracts.DTO;
using Constracts.EventCategory;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;

namespace Web.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly IEventCategoryService _categoryService;
        public NavbarViewComponent(IServiceManager serviceManager)
        {
            _categoryService = serviceManager.EventCategoryService;
        }
        

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<EventCategoryDTO> categories = await _categoryService.GetAllAsync();
            return View(categories.Select(c => new { Name = c.Name, Slug = c.Slug }).ToList());
        }
        
    }
}