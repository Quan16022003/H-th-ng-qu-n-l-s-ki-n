using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abtractions;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Admin.Controllers.ManageEvents
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IPathProvider _pathProvider;
        private readonly ICategoryEventService _categoryEventService;

        public EventController(
            [FromKeyedServices("Admin")] IPathProvider pathProvider, 
            IServiceManager serviceManager)
        {
            _pathProvider = pathProvider;
            _eventService = serviceManager.EventService;
            _categoryEventService = serviceManager.CategoryEventService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventsAsync();
            var a = await _categoryEventService.GetAllCategoryEventsAsync();
            return View($"{_pathProvider.GetViewsPath(this)}/Events.cshtml", events);
        }
    }
}
