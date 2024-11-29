using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EventVenueUpdateException : Exception
    {
        public EventVenueUpdateException(int eventId, string message, Exception innerException)
       : base($"Error updating event venue with id {eventId}: {message}", innerException)
        {
        }
    }
}
