using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
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

        private async Task<IEnumerable<EventDTO>> FetchEvents(string type = "", string query = "")
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
            await _eventService.DeleteEventAsyncById(id);
            return Ok(
                new {
                    message = "Delete event successfully"
                }
            );
        }
    }
}
