using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Controllers;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ContactController : BaseController
    {
        public ContactController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<ContactController>();
        }

        public IActionResult Index()
        {
            LoadCurrentUser();
            return View($"{ViewPath}/ContactIndex.cshtml");
        }
    }
}
