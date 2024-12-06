using Constracts.EventCategory;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.Abtractions;
using Web.Controllers;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageEvents
{
    [Authorize(Policy = "CategoryManagement")]
    [Area("Dashboard")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager, ILogger<CategoryController> logger) : base(serviceManager)
        {
            _logger = logger;
            ViewPath = pathProvideManager.Get<CategoryController>();
            _categoryService = serviceManager.CategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery(Name = "searchOption")] string searchType = "",
            [FromQuery(Name = "searchQuery")] string query = "")
        {
            var result = await _categoryService.GetAllAsync(searchType, query);
            if (result.IsFailure)
            {
                return BadRequest(result.Error.Message);
            }
            ViewBag.SearchOption = searchType;
            _logger.LogInformation("SearchOption: {searchOption}, SearchQuery: {searchQuery}", searchType, query);
            ViewBag.SearchQuery = query;
            return View($"{ViewPath}/Categories.cshtml", result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> ViewCategory(int id)
        {
            var category = await _categoryEventService.GetByIdAsync(id);
            return View($"{ViewPath}/ViewCategory.cshtml", category);
        }

        public IActionResult Add()
        {
            return View($"{ViewPath}/AddCategory.cshtml");
        }

        public async Task<IActionResult> HandleAdd(EventCategoryCreationDto data)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(new { message = errors.ToList() });
            }

            Result<int> result = await _categoryService.CreateAsync(data);
            if (result.IsFailure)
            {
                return BadRequest(new { message = result.Error.Message });
            }

            var successResponse = new 
            {
                message = "Thêm thành công",
                redirectUrl = Url.Action(nameof(Index), "Category", new { area = "Dashboard" })
            };
            return Ok(successResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            if (result.IsFailure)
            {
                return BadRequest(new { message = result.Error.Message });
            }
            return View($"{ViewPath}/UpdateCategory.cshtml", result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> HandleUpdate(EventCategoryUpdateDto data)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(new { message = errors.ToList() });
            }
            _logger.LogInformation("Updating category with Data: {data}", data.ToString());
            Result<int> result = await _categoryService.UpdateAsync(data);
            if (result.IsFailure)
            {
                return BadRequest(new { message = result.Error.Message });
            }
            var successResponse = new 
            {
                message = "Sửa thành công",
                redirectUrl = Url.Action(nameof(Index), "Category", new { area = "Dashboard" })
            };
            return Ok(successResponse);
        }

        [HttpDelete]
        public async Task<IActionResult> HandleDelete(int id)
        {
            Result result = await _categoryService.DeleteAsync(id);
            if (result.IsFailure)
            {
                return BadRequest(new { message = result.Error.Message });
            }
            
            var successResponse = new 
            {
                message = "Xoá thành công",
                redirectUrl = Url.Action(nameof(Index), "Category", new { area = "Dashboard" })
            };
            return Ok(successResponse);
        }
    }
}
