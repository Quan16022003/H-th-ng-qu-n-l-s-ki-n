using Constracts.DTO;
using EventManagementSystem.ViewModels;
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
            // Lấy dữ lieu từ csdl
            IEnumerable<EventDTO> featuredEvents = await _serviceManager.EventService.GetAllEventsAsync();
            // Chỉ lấy 3 event đầu tiên
            featuredEvents = featuredEvents.Take(3);
            // Chuyển đổi dữ liệu sang dạng ViewModel
            HomeViewModel viewModel = new()
            {
                FeaturedEvents = featuredEvents.Adapt<IEnumerable<EventCardViewModel>>()
            };
            JsonSerializerOptions jsonOptions = new() { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(featuredEvents, jsonOptions);
            Console.WriteLine(jsonString);
            return View(viewModel);
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
