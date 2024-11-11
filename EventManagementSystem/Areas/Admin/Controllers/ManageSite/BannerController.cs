using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Admin.Controllers.ManageSite
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly IPathProvider _pathProvider;

        public BannerController([FromKeyedServices("Admin")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public IActionResult Index()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Banners.cshtml");
        }
    }
}
