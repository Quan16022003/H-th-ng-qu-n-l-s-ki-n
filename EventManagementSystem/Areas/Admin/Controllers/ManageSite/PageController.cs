using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Admin.Controllers.ManageSite
{
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly IPathProvider _pathProvider;

        public PageController([FromKeyedServices("Admin")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public IActionResult Index()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Pages.cshtml");
        }
    }
}
