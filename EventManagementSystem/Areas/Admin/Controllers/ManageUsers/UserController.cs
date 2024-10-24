using Microsoft.AspNetCore.Mvc;
using Web.Utils;

namespace Web.Areas.Admin.Controllers.ManageUsers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly string views = PathProvider.GetAdminManageUsers();
        public IActionResult Index()
        {
            return View($"{views}/Users.cshtml");
        }
    }
}
