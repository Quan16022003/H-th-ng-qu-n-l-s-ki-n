using Constracts.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            ViewPath = pathProvideManager.Get<EventController>();
            _eventService = serviceManager.EventService;
        }

        private async Task<IEnumerable<EventDTO>> FetchEvents(string type = "", string query = "")
        {
            var events = await _eventService.GetAllEventsAsync();
            if (string.IsNullOrEmpty(query)) return events;

            if (type == "Equal")
            {
                return events.Where(e => e.Title != null 
                    && e.Title.Equals(query, StringComparison.CurrentCultureIgnoreCase));
            }
            else return events.Where(e => e.Title != null 
                    && e.Title.Contains(query, StringComparison.CurrentCultureIgnoreCase));
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

        [HttpPost]
        [Route("/dashboard/event/add/detail")]
        public async Task<IActionResult> HandleAddDetail(EventDetailDTO dto)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(new { message = errors.ToList() });
            }

            if (dto == null) return BadRequest(
                new
                {
                    message = "Model is null"
                });

            var result = await _eventService.AddEventAsync(dto);
            int id = result.Id;

            return Ok(new
            {
                message = "Save successfully",
                redirectUrl = Url.Action(nameof(Update), "Event", new
                {
                    area = "Dashboard",
                    id
                })
            });
        }

        public async Task<IActionResult> Update(int id)
        {
            EventDTO model = await _eventService.GetEventByIdAsync(id);
            return View($"{ViewPath}/UpdateEvent.cshtml", model);
        }

        [HttpPut]
        [Route("/dashboard/event/update/detail")]
        public async Task<IActionResult> HandleUpdateDetail(EventDetailDTO dto)
        {
            var currentUser = await UserService.GetCurrentUserAsync(User);
            dto.OrganizerId = currentUser.Id!;

            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(new { message = errors.ToList() });
            }

            await _eventService.UpdateEventDetailAsync(dto);

            return Ok(new
            {
                message = "Save successfully"
            });

        }

        [HttpPut]
        [Route("/dashboard/event/update/timing")]
        public async Task<IActionResult> HandleUpdateTiming(EventTimingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(new { message = errors.ToList() });
            }

            await _eventService.UpdateEventTiminglAsync(dto);
            return Ok(new
            {
                message = "Save Successfully"
            });
        }

        [HttpPut]
        [Route("/dashboard/event/update/venue")]
        public async Task<IActionResult> HandleUpdateVenue(EventVenueDTO dto)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(new { message = errors.ToList() });
            }

            await _eventService.UpdateEventVenueAsync(dto);
            return Ok(new
            {
                message = "Save Successfully"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Publish(int id)
        {
            var result = await _eventService.PublishEvent(id);
            if (result.IsFailure)
            {
                return BadRequest(
                    new
                    {
                        message = result.Error.Message,
                    });
            }

            return Ok(
                new
                {
                    message = "Publish Successfully",
                    redirectUrl = Url.Action(nameof(Index), "Event", new
                    {
                        area = "Dashboard"
                    })
                });
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
