using Microsoft.AspNetCore.Mvc;
using Web.Utils;

namespace Web.Areas.Admin.Controllers.ManageSite
{
    [Area("Admin")]
    public class PageController : Controller
    {
        private readonly string views = PathProvider.GetAdminManageSite();
        public IActionResult Index()
        {
            return View($"{views}/Pages.cshtml");
        }
    }
}
