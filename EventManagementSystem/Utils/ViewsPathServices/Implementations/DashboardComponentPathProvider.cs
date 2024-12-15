using Web.Areas.Dashboard.Views.Shared.Components.EventForm.BasicInformation;
using Web.Areas.Dashboard.Views.Shared.Components.ImageInput;
using Web.Areas.Dashboard.Views.Shared.Components.EventForm.Venue;
using Web.Areas.Dashboard.Views.Shared.Components.EventForm.Time;
using Web.Areas.Dashboard.Views.Shared.Components.EventForm.Ticket;

namespace Web.Utils.ViewsPathServices.Implementations
{
    public class DashboardComponentPathProvider : IPathProvider
    {
        private readonly string folder = "~/Areas/Dashboard/Views/Shared/Components";

        public string GetViewsPath(Type type)
        {
            string target = type.Name;
            return target switch
            {
                nameof(EventBasicInformation) => $"{folder}/EventForm/BasicInformation",
                nameof(EventVenueForm) => $"{folder}/EventForm/Venue",
                nameof(EventTimeForm) => $"{folder}/EventForm/Time",
                nameof(EventTicketList) => $"{folder}/EventForm/Ticket", 
                nameof(ImageInputViewComponent) => $"{folder}/ImageInput",
                _ => throw new ArgumentException($"Does not found Component {target}")
            };
        }
    }
}
