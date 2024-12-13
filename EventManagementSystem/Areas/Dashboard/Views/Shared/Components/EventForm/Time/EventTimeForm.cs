using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Views.Shared.Components.EventForm.Time
{
    [Area("Dashboard")]
    public class EventTimeForm : ViewComponent
    {
        private readonly string viewPath;
        private readonly IEventService _eventService;

        public EventTimeForm(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager)
        {
            viewPath = pathProvideManager.Get<EventTimeForm>();
            _eventService = serviceManager.EventService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = await LoadModel(id);
            return View($"{viewPath}/EventTimeForm.cshtml", model);
        }

        private async Task<EventTimingDTO> LoadModel(int id)
        {
            EventDTO model = await _eventService.GetEventByIdAsync(id);
            return new EventTimingDTO
            {
                Id = id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                CreatedDate = model.CreatedDate,
                IsDeleted = model.IsDeleted,
                ModifiedDate = model.ModifiedDate,
            };
        }
    }
}
