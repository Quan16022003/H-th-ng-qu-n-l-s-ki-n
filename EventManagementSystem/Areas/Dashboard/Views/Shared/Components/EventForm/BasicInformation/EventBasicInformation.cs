using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Areas.Dashboard.ViewModels;
using Web.Areas.Dashboard.ViewModels.EventForm;
using Web.Authorize;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Views.Shared.Components.EventForm.BasicInformation
{
    [Area("Dashboard")]
    [ViewComponent(Name = "EventBasicInformationForm")]
    public class EventBasicInformation : ViewComponent
    {
        private readonly string viewPath;
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;

        public EventBasicInformation(
            IServiceManager serviceManager,
            IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<EventBasicInformation>();
            _categoryService = serviceManager.CategoryService;
            _userService = serviceManager.UserService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await LoadModel();
            return View($"{viewPath}/EventBasicInformation.cshtml", model);
        }

        private async Task<EventBasicInformationViewModel> LoadModel()
        {
            if (User.Identity == null) return null!;

            var categories = (await _categoryService.GetAllAsync()).Value;
            var currentUser = await _userService.GetCurrentUserAsync(HttpContext.User);

            return new EventBasicInformationViewModel
            {
                Categories = categories,
                Organizer = currentUser
            };
        }
    }
}
