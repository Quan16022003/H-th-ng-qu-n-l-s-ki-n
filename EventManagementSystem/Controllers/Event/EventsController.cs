using Constracts.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Utils.ViewsPathServices;
using Web.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Web.Controllers.Event
{
    public class EventsController : BaseController
    {
        private readonly IEventService _eventService;
        private readonly ILogger<HomeController> _logger;
        public EventsController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<EventsController>();
            _eventService = serviceManager.EventService;
        }

        [HttpGet]
        [Route("/events")]
        public async Task<IActionResult> AllEventAsync()
        {
            var vm = new AllEventViewModel();
            vm.AllEvents = await _eventService.GetAllEventsSelectedAsync(null, null, null, null, null);

            return View($"{ViewPath}/AllEvent.cshtml", vm);
        }


        /*public async Task<IActionResult> Index(string query, int? categoryId, string city, DateTime? startDate, DateTime? endDate)
        *//*{*//*
            try
            {
                var events = await _eventService.GetAllEventsSelectedAsync(query, categoryId, city, startDate, endDate);

                var formDTO = new EventFilterDTO
                {
                    Query = query,
                    CategoryId = categoryId,
                    City = city,
                    StartDate = startDate,
                    EndDate = endDate
                };


                return View(new EventFilterViewModel
                {
                    Event = events,
                    Form = formDTO

                });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Lỗi khi lấy danh sách sự kiện.");
                return View("Error");
            }
        }*/


        [HttpGet]
        [Route("/Details/{slug}")]
        public async Task<IActionResult> DetailEventAsync(string slug)
        {
            var vm = new DetailEventViewModel();
            vm.Details = await _eventService.GetEventBySlugAsync(slug);

            if (vm.Details == null)
            {
                return NotFound();
            }

            return View($"{ViewPath}/DetailEvent.cshtml", vm);
        }

        [HttpGet]
        [Route("/Map")]
        public async Task<IActionResult> Maps()
        {

            return View($"{ViewPath}/Maps.cshtml");
        }
        [HttpGet]
        public IActionResult About()
        {
            return View($"{ViewPath}/About.cshtml");
        }
        [HttpGet]
        public IActionResult BlogEvent()
        {
            return View($"{ViewPath}/BlogEvent.cshtml");
        }
        [HttpGet]
        public IActionResult DetailMap()
        {
            return View($"{ViewPath}/DetailMap.cshtml");
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            return View($"{ViewPath}/Privacy.cshtml");
        }
        [HttpGet]
        public IActionResult Terms()
        {
            return View($"{ViewPath}/Terms.cshtml");
        }
    }
}
