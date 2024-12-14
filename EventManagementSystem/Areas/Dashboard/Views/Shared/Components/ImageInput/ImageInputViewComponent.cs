using Microsoft.AspNetCore.Mvc;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Views.Shared.Components.ImageInput
{
    [Area("Dashboard")]
    public class ImageInputViewComponent : ViewComponent
    {
        private readonly string viewPath;

        public ImageInputViewComponent(IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<ImageInputViewComponent>();
        }

        public IViewComponentResult Invoke(string image)
        {
            return View($"{viewPath}/ImageInput.cshtml", image);
        }
    }
}
