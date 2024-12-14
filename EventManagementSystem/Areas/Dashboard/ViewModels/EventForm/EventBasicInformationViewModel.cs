using Constracts.DTO;
using Constracts.EventCategory;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Dashboard.ViewModels.EventForm
{
    public class EventBasicInformationViewModel
    {
        public EventDetailDTO? EventDetail;
        public string? ThumbnailUrl { get; set; }
        public required IEnumerable<EventCategoryDTO> Categories { get; set; }
        public required UserDTO Organizer {  get; set; }
    }
}
