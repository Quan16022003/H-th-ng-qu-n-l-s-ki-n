using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Utils;
using Web.Utils.ViewsPathServices;
using Constracts.DTO;
using Services.Abtractions;
using Web.Controllers;

namespace Web.Areas.Dashboard.Controllers.ManageUsers
{
    [Area("Dashboard")]
    public class UserController : BaseController
    {
        public UserController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<UserController>();
        }

        private async Task<IEnumerable<UserDTO>> FetchUsers(string type = "", string query = "")
        {
            var users = await UserService.GetAllUsersAsync();
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
            LoadCurrentUser();
            var users = await FetchUsers(searchType, query);
            return View($"{ViewPath}/Users.cshtml", users);
        }
    }
}
