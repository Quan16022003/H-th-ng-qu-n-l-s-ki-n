using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ContactController : Controller
    {
        private readonly string viewPath;

        public ContactController(IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<ContactController>();
        }

        public IActionResult Index()
        {
            return View($"{viewPath}/ContactIndex.cshtml");
        }
    }
}
