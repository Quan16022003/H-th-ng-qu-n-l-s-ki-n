using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;


namespace Web.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly string viewPath;

        public AccountController(IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<AccountController>();
        }
        [HttpGet]
        public IActionResult Login(string username, string password)
        {
            // Xử lý đăng nhập ở đây
            // Nếu thành công, chuyển hướng đến trang chính hoặc trang khác
            return View($"{viewPath}/Login.cshtml");
            //return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View($"{viewPath}/Register.cshtml");
        }

    }
}
