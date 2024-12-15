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
    [Authorize(Policy = "TicketManagement")]
    [Area("Dashboard")]
    public class TicketController : BaseController
    {
        private readonly ITicketService _ticketService;

        public TicketController(
            IPathProvideManager pathProvideManager,
            IServiceManager serviceManager) : base(serviceManager)
        {
            ViewPath = pathProvideManager.Get<TicketController>();
            _ticketService = serviceManager.TicketService;
        }

        private async Task<IEnumerable<TicketDTO>> FetchTickets(string type = "", string query = "")
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            if (string.IsNullOrEmpty(query)) return tickets;

            if (type == "Equal")
            {
                return tickets.Where(e => e.Title!.Equals(query, StringComparison.CurrentCultureIgnoreCase));
            }
            else return tickets.Where(e => e.Title!.Contains(query, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<IActionResult> Index(
            [FromQuery(Name = "searchOption")] string searchType = "",
            [FromQuery(Name = "searchQuery")] string query = "")
        {
            var tickets = await FetchTickets(searchType, query);
            return View($"{ViewPath}/Ticket.cshtml", tickets);
        }

        [HttpGet]
        [Route("/dashboard/ticket/event/{id?}")]
        public IActionResult EventTickets(int id)
        {
            return ViewComponent("EventTicketList", id);
        }

        [HttpGet]
        [Route("/dashboard/ticket/update-form/{id?}")]
        public async Task<IActionResult> UpdateForm(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            return PartialView("./Partials/_EventTicketForm", ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TicketDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(
                    new
                    {
                        message = "Add ticket failed"
                    });
            }

            await _ticketService.AddTicketAsync(dto);

            return Ok(
                new
                {
                    message = "Save Successfully"
                });
        }

        [HttpPut]
        public async Task<IActionResult> Update(TicketDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(
                    new
                    {
                        message = "Update ticket failed"
                    });
            }

            await _ticketService.UpdateTicketAsync(dto);

            return Ok(
                new
                {
                    message = "Save Successfully"
                });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return Ok(
                new
                {
                    message = "Delete Successfully"
                });
        }
    }
}
