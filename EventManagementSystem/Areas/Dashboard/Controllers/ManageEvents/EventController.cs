using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abtractions;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageEvents
{
    [Area("Dashboard")]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly string viewPath;

        public EventController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager)
        {
            _eventService = serviceManager.EventService;
            viewPath = pathProvideManager.Get<EventController>("Dashboard");
        }

        private async Task<IEnumerable<Domain.Entities.Events>> FetchEvents(string type = "", string query = "")
        {
            var events = await _eventService.GetAllEventsAsync();
            if (string.IsNullOrEmpty(query)) return events;

            if (type == "Equal")
            {
                return events.Where(e => e.Title.Equals(query, StringComparison.CurrentCultureIgnoreCase));
            }
            else return events.Where(e => e.Title.Contains(query, StringComparison.CurrentCultureIgnoreCase));
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            [FromQuery(Name = "searchOption")] string searchType = "",
            [FromQuery(Name = "searchQuery")] string query = "")
        {
            var events = await FetchEvents(searchType, query);
            return View($"{viewPath}/Events.cshtml", events);
        }

        public IActionResult Add()
        {
            return View($"{viewPath}/AddEvent.cshtml");
        }

        // call by Ajax
        [HttpDelete]
        public async Task<IActionResult> HandleDelete(int id)
        {
            var model = await _eventService.GetEventByIdAsync(id);

            if (model == null) return NotFound();
            if (model.IsDeleted) return NoContent();

            await _eventService.DeleteEventAsync(model);
            return NoContent();
        }
    }
}
