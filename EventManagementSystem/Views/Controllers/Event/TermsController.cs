using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Event.Controllers.Event
{
    [Area("Event")]
    public class TermsController : Controller
    {
        private IPathProvider _pathProvider;

        public TermsController([FromKeyedServices("Terms")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        [HttpGet]
        public IActionResult Terms()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Terms.cshtml");
        }
    }
}
