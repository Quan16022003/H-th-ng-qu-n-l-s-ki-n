using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Areas.Dashboard.ViewModels;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageUsers
{
    [Area("Dashboard")]
    public class UserController : Controller
    {
        private readonly string viewPath;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(
            IPathProvideManager pathProvideManager,
            UserManager<ApplicationUser> userManager)
        {
            viewPath = pathProvideManager.Get<UserController>("Dashboard");
            _userManager = userManager;
        }

        private async Task<IEnumerable<UserViewModel>> FetchUsers(string type = "", string query = "")
        {
            var users = await _userManager.Users.ToListAsync();
            List<UserViewModel> viewModels = [];

            foreach (var user in users)
            {
                var userViewModel = UserViewModelMapper.Map(user);
                userViewModel.Role = string.Join(", ", await _userManager.GetRolesAsync(user));

                viewModels.Add(userViewModel);
            }

            if (string.IsNullOrEmpty(query)) return viewModels;

            if (type == "Equal")
            {
                return viewModels.Where(e => e.Username!.Equals(query, StringComparison.CurrentCultureIgnoreCase));
            }
            else return viewModels.Where(e => e.Username!.Contains(query, StringComparison.CurrentCultureIgnoreCase));
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
