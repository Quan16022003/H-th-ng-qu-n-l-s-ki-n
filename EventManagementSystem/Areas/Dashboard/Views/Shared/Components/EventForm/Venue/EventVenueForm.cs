using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Views.Shared.Components.EventForm.Venue
{
    [Area("Dashboard")]
    [ViewComponent(Name = "EventVenueForm")]
    public class EventVenueForm : ViewComponent
    {
        private readonly string viewPath;

        public EventVenueForm(IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<EventVenueForm>();
        }

        public IViewComponentResult Invoke()
        {
            return View($"{viewPath}/EventVenueForm.cshtml");
        }
    }
}
