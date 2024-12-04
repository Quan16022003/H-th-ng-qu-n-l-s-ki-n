using Constracts.DTO;
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
            ViewPath = pathProvideManager.Get<EventController>();
            _eventService = serviceManager.EventService;
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

        [HttpPost]
        [Route("/dashboard/event/add/detail")]
        public async Task<IActionResult> HandleAddDetail(EventDetailDTO dto)
        {
            var currentUser = await UserService.GetCurrentUserAsync(User);
            dto.OrganizerId = currentUser.Id!;

            if (dto == null) return BadRequest(
                new
                {
                    message = "Model is null"
                });

            await _eventService.AddEventAsync(dto);
            return Ok(new
            {
                message = "Save successfully"
            });
        }

        //[HttpPost]
        //public async Task<IActionResult> HandleAdd(EventDetailDTO dto)
        //{
        //    if (dto == null)
        //    {
        //        return BadRequest(new {
        //           message = "Event is null"
        //        });
        //    }
        //    await _eventService.GetAllEventsAsync();
        //    return Ok();

        //    //await _eventService.AddEventAsync(dto);
        //    //return Ok(new
        //    //{
        //    //    message = "Create Successfully",
        //    //    redirectUrl = Url.Action(nameof(Index), "Event", new
        //    //    {
        //    //        area = "Dashboard"
        //    //    })
        //    //});
        //}

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
