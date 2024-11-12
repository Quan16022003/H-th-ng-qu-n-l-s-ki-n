using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Admin.Controllers.ManageEvents
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IPathProvider _pathProvider;

        public CategoryController([FromKeyedServices("Admin")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public IActionResult Index()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Categories.cshtml");
        }
    }
}
