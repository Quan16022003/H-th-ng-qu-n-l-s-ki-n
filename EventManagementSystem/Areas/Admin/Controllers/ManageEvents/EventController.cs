using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Admin.Controllers.ManageEvents
{
    [Area("Admin")]
    public class EventController : Controller
    {
        private readonly IPathProvider _pathProvider;

        public EventController([FromKeyedServices("Admin")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public IActionResult Index()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Events.cshtml");
        }
    }
}
