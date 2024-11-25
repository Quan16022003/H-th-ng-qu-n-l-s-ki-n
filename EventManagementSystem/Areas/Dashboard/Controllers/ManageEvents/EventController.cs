using Constracts.DTO;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Controllers;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageEvents
{
    [Area("Dashboard")]
    [Authorize(Policy = "EventManagement")]
    public class EventController : BaseController
    {
        private readonly IEventService _eventService;

        public EventController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            _eventService = serviceManager.EventService;
            ViewPath = pathProvideManager.Get<EventController>();
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
            return View($"{ViewPath}/Events.cshtml", events);
        }

        public IActionResult Add()
        {
            return View($"{ViewPath}/AddEvent.cshtml");
        }

        // call by Ajax
        [HttpDelete]
        public async Task<IActionResult> HandleDelete(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return Ok(
                new {
                    message = "Delete event successfully"
                }
            );
        }
    }
}
