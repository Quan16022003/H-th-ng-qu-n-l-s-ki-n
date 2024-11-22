using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

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
