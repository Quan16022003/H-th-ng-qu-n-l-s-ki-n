using Microsoft.AspNetCore.Mvc;
using Web.Utils;

namespace Web.Areas.Admin.Controllers.ManageEvents
{
    [Area("Admin")]
    public class CagetoryController : Controller
    {
        private readonly string views = PathProvider.GetAdminManageEvents();
        public IActionResult Index()
        {
            return View($"{views}/Cagetories.cshtml");
        }
    }
}
