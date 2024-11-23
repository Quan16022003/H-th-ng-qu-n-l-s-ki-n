using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Profile
{
    public class ProfileController : Controller
    {
        public IActionResult Information()
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
