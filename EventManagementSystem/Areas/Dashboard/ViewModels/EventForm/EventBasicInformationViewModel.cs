using Constracts.DTO;
using Constracts.EventCategory;

namespace Web.Areas.Dashboard.ViewModels.EventForm
{
    public class EventBasicInformationViewModel
    {
        public required IEnumerable<EventCategoryDTO> Categories { get; set; }
        public required UserDTO Organizer {  get; set; }
        public EventDTO? Event { get; set; }
    }
}
