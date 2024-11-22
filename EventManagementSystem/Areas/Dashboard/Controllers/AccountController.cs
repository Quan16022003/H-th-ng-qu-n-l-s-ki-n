using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Dashboard.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Domain.Enum;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnURL = returnUrl;
            return View();
        }
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            returnUrl ??= Url.Action("Index", "Event");
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Console.WriteLine(model.Email);
            var user = await _signInManager.UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {   
                ModelState.AddModelError(string.Empty, "Tài khoản không tồn tại");
                return View(model);
            }
            
            var result = await _signInManager.PasswordSignInAsync(model.Email,
                                model.Password, false, lockoutOnFailure: false);
            Console.WriteLine("Result: " + result);
            if (result.Succeeded)
            {
                if (User.IsInRole(Roles.Administrator) || User.IsInRole(Roles.Organizer))
                {
                    if (returnUrl != null)
                    {
                        return LocalRedirect(returnUrl);
                    }
                }
                ModelState.AddModelError(string.Empty, "Bạn không có quyền truy cập");
                foreach (Claim userClaim in User.Claims)
                {
                    ModelState.AddModelError(string.Empty, userClaim.Value);
                }
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                return View(model);
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản của bạn đã bị khóa");
                return View(model);
            }
            
            ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login");
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult AccessDenied()
        {
            ViewBag.ReturnUrl = Url.Action("Index", "Statistics") ?? string.Empty;
            return View();
        }
    }
}