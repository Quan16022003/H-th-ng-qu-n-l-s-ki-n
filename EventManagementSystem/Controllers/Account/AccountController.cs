using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Utils.ViewsPathServices;


namespace Web.Controllers.Account
{
    public class AccountController : BaseController
    {
        public AccountController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<AccountController>();
        }
        [HttpGet]
        public IActionResult Login(string username, string password)
        {
            // Xử lý đăng nhập ở đây
            // Nếu thành công, chuyển hướng đến trang chính hoặc trang khác
            return View($"{ViewPath}/Login.cshtml");
            //return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View($"{ViewPath}/Register.cshtml");
        }

    }
}
