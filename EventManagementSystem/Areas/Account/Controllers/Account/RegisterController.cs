using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

[Area("Account")]
public class RegisterController : Controller
{
    private readonly IPathProvider _pathProvider;
    public RegisterController([FromKeyedServices("Account")] IPathProvider pathProvider)
    {
        _pathProvider = pathProvider;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View($"{_pathProvider.GetViewsPath(this)}/Register.cshtml");
    }
}
