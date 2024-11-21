using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;


namespace Web.Views.Controllers.Event
{
    [Area("Account")]
    public class AccountController : Controller
    {
        private IPathProvider _pathProvider;

        public AccountController([FromKeyedServices("Account")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }
       [HttpGet]
        public IActionResult Login(string username, string password)
        {
            // Xử lý đăng nhập ở đây
            // Nếu thành công, chuyển hướng đến trang chính hoặc trang khác
            return View($"{_pathProvider.GetViewsPath(this)}/Login.cshtml");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View($"{_pathProvider.GetViewsPath(this)}/Register.cshtml");
        }
        
    }
}
