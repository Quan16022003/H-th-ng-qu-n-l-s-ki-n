using Constracts.DTO;
using Constracts.EventCategory;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;

namespace Web.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public NavbarViewComponent(IServiceManager serviceManager)
        {
            _categoryService = serviceManager.CategoryService;
        }
        

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _categoryService.GetAllAsync();
            var categories = result.Value;
            return View(categories.Select(c => new { Name = c.Name, Slug = c.Slug }).ToList());
        }
        
    }
}