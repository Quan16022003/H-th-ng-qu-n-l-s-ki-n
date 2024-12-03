using Constracts.DTO;
using Constracts.EventCategory;
using Web.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using System.Diagnostics;
using Web.Models;
using System.Text.Json;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceManager _serviceManager;
        public HomeController(
            ILogger<HomeController> logger, 
            IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                HomeViewModel viewModel = new()
                {
                    Venues = await _serviceManager.VenueService.GetCitiesSortedByEventCountAsync(),
                    FeaturedEvents = await _serviceManager.EventService.GetAllEventsOutstandingAsync(),
                    UpcomingEvents = await _serviceManager.EventService.GetAllEventsComingAsync(),
                    BestSellerEvents = await _serviceManager.EventService.GetAllEventsBestSellingAsync(),
                };
                var catogoryResult = await _serviceManager.CategoryService.GetAllAsync();
                viewModel.EventCategoríes = (catogoryResult.IsSuccess) ? catogoryResult.Value : new List<EventCategoryDTO>();
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in Home Controller");
                return View(new HomeViewModel());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
