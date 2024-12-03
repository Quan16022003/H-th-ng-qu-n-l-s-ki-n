using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abtractions
{
    public interface IServiceManager
    {
        IUserService UserService { get; }
        IEventService EventService { get; }
        ICategoryService CategoryService { get; }
        ITicketService TicketService { get; }
        IAttendeeService AttendeeService { get; }
        IVenueService VenueService { get; }
    }
}
