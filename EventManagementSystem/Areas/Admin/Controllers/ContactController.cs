using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IPathProvider _pathProvider;

        public ContactController([FromKeyedServices("Admin")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public IActionResult Index()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/ContactIndex.cshtml");
        }
    }
}
