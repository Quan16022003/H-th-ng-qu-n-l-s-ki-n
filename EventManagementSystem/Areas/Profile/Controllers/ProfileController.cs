using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class ProfileController : Controller
    {
        private readonly IPathProvider _pathProvider;

        public ProfileController([FromKeyedServices("Profile")] IPathProvider pathProvider)
        {
            _pathProvider = pathProvider;
        }

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
