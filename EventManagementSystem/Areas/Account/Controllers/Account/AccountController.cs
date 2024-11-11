using Microsoft.AspNetCore.Mvc;

[Area("Account")]
public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        // Xử lý đăng nhập ở đây
        // Nếu thành công, chuyển hướng đến trang chính hoặc trang khác
        return RedirectToAction("Index", "Home");
    }
}

