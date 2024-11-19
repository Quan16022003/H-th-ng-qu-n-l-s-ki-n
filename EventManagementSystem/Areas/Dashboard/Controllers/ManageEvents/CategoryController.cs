using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abtractions;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageEvents
{
    [Area("Dashboard")]
    public class CategoryController : Controller
    {
        //private readonly ICategoryEventService _categoryEventService;
        private readonly string viewPath;

        public CategoryController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager)
        {
            viewPath = pathProvideManager.Get<CategoryController>("Dashboard");
            //_categoryEventService = serviceManager.CategoryEventService;
        }

        //private async Task<IEnumerable<Domain.Entities.CategoryEvents>> FetchCategories(string type = "", string query = "")
        //{
        //    //var events = await _categoryEventService.GetAllCategoryEventsAsync();
        //    //if (string.IsNullOrEmpty(query)) return events;

        //    if (type == "Equal")
        //    {
        //        return events.Where(e => e.Name.Equals(query, StringComparison.CurrentCultureIgnoreCase));
        //    }
        //    else return events.Where(e => e.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase));
        //}

        public async Task<IActionResult> Index(
            [FromQuery(Name = "searchOption")] string searchType = "",
            [FromQuery(Name = "searchQuery")] string query = "")
        {
            //var categories = await FetchCategories(searchType, query);

            return View($"{viewPath}/Categories.cshtml");
        }
    }
}
