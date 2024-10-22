using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void UpdatePersonalDetail()
        {

        }

        [HttpPost]
        public void UpdatePersonalSecurity()
        {

        }
    }
}
