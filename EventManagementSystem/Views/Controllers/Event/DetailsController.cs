using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Event.Controllers.Event
{
    [Area("Event")]
    public class DetailsController : Controller
    {
        private IPathProvider _pathProvider;

        public DetailsController([FromKeyedServices("Detail")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        [HttpGet]
        public IActionResult DetailEvent()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/DetailEvent.cshtml");
        }
    }
}
