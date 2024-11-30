using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Utils.ViewsPathServices;
using Web.ViewModels;


namespace Web.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private Logger<AccountController> _logger;
        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILoggerFactory loggerFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = new Logger<AccountController>(loggerFactory);
        }
        
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return RedirectToAction("Index", "Home"); // Redirect to Home if already logged in
            }

            LoginViewModel viewModel = new()
            {
                ReturnUrl = returnUrl ?? Url.Content("~/")
            };
            return View(viewModel);
        }
        
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result =  await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return LocalRedirect(model.ReturnUrl);
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Tài khoản của bạn đã bị khóa.");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin.");
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Register(string? returnUrl)
        {
            return View(new RegisterViewModel() { ReturnUrl = returnUrl ?? Url.Content("~/") });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ApplicationUser? existingUser = await _userManager.FindByEmailAsync(model.Email);
            if ( existingUser is not null)
            {
                ModelState.AddModelError("Email", "Email này đã được sử dụng");
                return View(model);
            }
            ApplicationUser user = new()
            {
                UserName = model.Email, Email = model.Email,
                FirstName = model.FirstName, LastName = model.LastName
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(model.ReturnUrl);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
