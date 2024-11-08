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

        public EventController(
            [FromKeyedServices("Admin")] IPathProvider pathProvider, 
            IServiceManager eventService)
        {
            _pathProvider = pathProvider;
            _eventService = eventService.EventService;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventsAsync();
            return View($"{_pathProvider.GetViewsPath(this)}/Events.cshtml", events);
        }
    }
}
