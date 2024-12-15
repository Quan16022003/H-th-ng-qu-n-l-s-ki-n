using Constracts.DTO;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abtractions;
using Web.Controllers;
using Web.Utils;
using Web.Utils.ViewsPathServices;

namespace Web.Areas.Dashboard.Controllers.ManageBookings
{
    [Authorize(Policy = "OrderManagement")]
    [Area("Dashboard")]
    public class BookingController : BaseController
    {
        private readonly IOrdersService _orderService;

        public BookingController(
            IPathProvideManager pathProviderManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProviderManager.Get<BookingController>();
            _orderService = serviceManager.OrdersService;
        }

        private async Task<IEnumerable<OrderDTO>> FetchOrders(string type = "", string query = "")
        {
            var orders = await _orderService.GetOrdersAsync();
            if (string.IsNullOrEmpty(query)) return orders;

            if (type == "Equal")
            {
                return orders.Where(e => e.FirstName!.Equals(query, StringComparison.CurrentCultureIgnoreCase)
                || e.LastName!.Equals(query, StringComparison.CurrentCultureIgnoreCase));
            }
            else return orders.Where(e => e.FirstName!.Contains(query, StringComparison.CurrentCultureIgnoreCase)
            || e.LastName!.Contains(query, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<IActionResult> Index(
            [FromQuery(Name = "searchOption")] string searchType = "",
            [FromQuery(Name = "searchQuery")] string query = "")
        {
            var orders = await FetchOrders(searchType, query);
            return View($"{ViewPath}/Bookings.cshtml", orders);
        }
    }
}
