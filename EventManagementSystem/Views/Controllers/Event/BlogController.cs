using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Event.Controllers.Event
{
    [Area("Event")]
    public class BlogController : Controller
    {
        private IPathProvider _pathProvider;

        public BlogController([FromKeyedServices("Blog")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        [HttpGet]
        public IActionResult BlogEvent()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/BlogEvent.cshtml");
        }
    }
}
