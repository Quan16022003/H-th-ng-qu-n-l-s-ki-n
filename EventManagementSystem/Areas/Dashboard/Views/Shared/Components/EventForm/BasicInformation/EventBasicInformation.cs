﻿using Constracts.DTO;
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
        private readonly IEventService _eventService;

        public EventBasicInformation(
            IServiceManager serviceManager,
            IPathProvideManager pathProvideManager)
        {
            viewPath = pathProvideManager.Get<EventBasicInformation>();
            _categoryService = serviceManager.CategoryService;
            _eventService = serviceManager.EventService;
            _userService = serviceManager.UserService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id = -1)
        {
            var viewModel = await LoadModel(id);
            return View($"{viewPath}/EventBasicInformation.cshtml", viewModel);
        }

        /// <summary>
        /// Loading model for view component, if id is -1 (user is create new model), return with event is null
        /// </summary>
        /// <param name="id">Event Id</param>
        /// <returns></returns>
        private async Task<EventBasicInformationViewModel> LoadModel(int id)
        {
            if (User.Identity == null) return null!;

            var categories = (await _categoryService.GetAllAsync()).Value;
            var currentUser = await _userService.GetCurrentUserAsync(HttpContext.User);

            EventDTO? eventModel = id != -1 ? await _eventService.GetEventByIdAsync(id) : null;

            return new EventBasicInformationViewModel
            {
                EventDetail = new EventDetailDTO
                {
                    Title = eventModel != null ? eventModel.Title : string.Empty,
                    Description = eventModel != null ? eventModel.Description : string.Empty,
                    CategoryId = eventModel?.CategoryEvent?.Id ?? -1
                },
                ThumbnailUrl = eventModel != null ? eventModel.ThumbnailUrl : string.Empty,
                Categories = categories,
                Organizer = currentUser,
            };
        }
    }
}
