using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Views.Shared.Components.EventForm.Ticket
{
    [Area("Dashboard")]
    [ViewComponent(Name = "EventTicketList")]
    public class EventTicketList : ViewComponent
    {
        private readonly string viewPath;
        private readonly ITicketService _ticketService;

        public EventTicketList(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager)
        {
            viewPath = pathProvideManager.Get<EventTicketList>();
            _ticketService = serviceManager.TicketService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int eventId)
        {
            var tickets = await LoadModel(eventId);
            return View($"{viewPath}/EventTicketList.cshtml", tickets);
        }

        private async Task<IEnumerable<TicketDTO>> LoadModel(int eventId)
        {
            return await _ticketService.GetAllTicketsByEventIdAsync(eventId);
        }
    }
}
