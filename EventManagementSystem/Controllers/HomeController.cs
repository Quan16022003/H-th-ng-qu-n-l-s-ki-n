using Constracts.DTO;
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
            HomeViewModel viewModel = new()
            {
                FeaturedEvents = await _serviceManager.EventService.GetAllEventsComingAsync()
            };
            
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
