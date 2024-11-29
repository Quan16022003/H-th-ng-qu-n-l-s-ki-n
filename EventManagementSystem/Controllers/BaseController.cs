using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
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
        
        protected readonly IUserService UserService;

        public BaseController(IServiceManager serviceManager)
        {
            UserService = serviceManager.UserService;
        }
    }
}
