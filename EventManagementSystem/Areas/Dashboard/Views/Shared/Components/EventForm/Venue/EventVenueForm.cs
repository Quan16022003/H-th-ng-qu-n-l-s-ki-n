using Constracts.DTO;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Views.Shared.Components.EventForm.Venue
{
    [Area("Dashboard")]
    [ViewComponent(Name = "EventVenueForm")]
    public class EventVenueForm : ViewComponent
    {
        private readonly string viewPath;
        private readonly IEventService _eventService;

        public EventVenueForm(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager)
        {
            viewPath = pathProvideManager.Get<EventVenueForm>();
            _eventService = serviceManager.EventService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var model = await LoadModel(id);
            return View($"{viewPath}/EventVenueForm.cshtml", model);
        }

        private async Task<EventVenueDTO> LoadModel(int id)
        {
            EventDTO? model = await _eventService.GetEventByIdAsync(id);
            return model.Adapt<EventVenueDTO>();
        }
    }
}
