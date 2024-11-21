using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Utils;
using Web.Utils.ViewsPathServices;
using Constracts.DTO;
using Services.Abtractions;

namespace Web.Areas.Dashboard.Controllers.ManageUsers
{
    [Area("Dashboard")]
    public class UserController : Controller
    {
        private readonly string viewPath;
        private readonly IUserService _userService;

        public UserController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager)
        {
            viewPath = pathProvideManager.Get<UserController>();
            _userService = serviceManager.UserService;
        }

        private async Task<IEnumerable<UserDTO>> FetchUsers(string type = "", string query = "")
        {
            var users = await _userService.GetAllUsersAsync();
            if (string.IsNullOrEmpty(query)) return users;

            if (type == "Equal")
            {
                return users.Where(e => e.UserName!.Equals(query, StringComparison.CurrentCultureIgnoreCase));
            }
            else return users.Where(e => e.UserName!.Contains(query, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<IActionResult> Index(
            [FromQuery(Name = "searchOption")] string searchType = "",
            [FromQuery(Name = "searchQuery")] string query = "")
        {
            var users = await FetchUsers(searchType, query);
            return View($"{viewPath}/Users.cshtml", users);
        }
    }
}
