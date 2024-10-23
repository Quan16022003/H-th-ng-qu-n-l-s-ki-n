using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Models;  // Import ApplicationUser

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager; 
    private readonly UserManager<ApplicationUser> _userManager;  // Thêm UserManager để quản lý người dùng

    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;  // Gán giá trị cho UserManager
    }

    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);  // Tìm người dùng theo email
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        ModelState.AddModelError(string.Empty, "Đăng nhập thất bại. Vui lòng kiểm tra lại thông tin.");
        return View();
    }

    // POST: /Account/Logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
