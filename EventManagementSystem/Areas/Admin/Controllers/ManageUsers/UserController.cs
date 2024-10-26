using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Admin.Controllers.ManageUsers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IPathProvider _pathProvider;

        public UserController([FromKeyedServices("Admin")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

        public IActionResult Index()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Users.cshtml");
        }
    }
}
