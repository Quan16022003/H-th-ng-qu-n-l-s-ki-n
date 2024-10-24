using Microsoft.AspNetCore.Mvc;
using Web.Utils;

namespace Web.Areas.Admin.Controllers.ManageSite
{
    [Area("Admin")]
    public class MediaController : Controller
    {
        private readonly string views = PathProvider.GetAdminManageSite();
        public IActionResult Index()
        {
            return View($"{views}/Media.cshtml");
        }
    }
}
