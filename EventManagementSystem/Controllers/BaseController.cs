using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abtractions;
using System.Security.Claims;
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
            if (User == null) return;

            CurrentUser = UserService.GetCurrentUserAsync(User).Result;
            Permissions = new AccessPermission(CurrentUser);

            ViewBag.CurrentUser = CurrentUser;
            ViewBag.Permissions = Permissions;
        }
    }
}
