using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EventDetailUpdateException : Exception
    {
        public EventDetailUpdateException(int eventId, string message, Exception innerException)
       : base($"Error updating event detail with id {eventId}: {message}", innerException)
        {
        }
    }
}
