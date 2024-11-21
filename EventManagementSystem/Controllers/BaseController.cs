using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abtractions;
using Web.Authorize;

namespace Web.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Path to folder contain the view
        /// </summary>
        protected string? ViewPath { get; init; }
        protected UserDTO? CurrentUser { get; private set; }
        protected AccessPermission? Permissions { get; private set; }
        
        protected readonly IUserService UserService;

        public BaseController(IServiceManager serviceManager)
        {
            UserService = serviceManager.UserService;
        }

        protected void LoadCurrentUser()
        {
            //if (User == null) return;
            //CurrentUser = userService.GetCurrentUserAsync(User).Result;

            CurrentUser = UserService.GetUserByIdAsync("8e445865-a24d-4543-a6c6-9443d048cdb9").Result;
            Permissions = new AccessPermission(CurrentUser);

            ViewBag.CurrentUser = CurrentUser;
            ViewBag.Permissions = Permissions;
        }
    }
}
