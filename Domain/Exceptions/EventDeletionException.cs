using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EventDeletionException: Exception
    {
        public EventDeletionException(int eventId, string message, Exception innerException)
        : base($"Error deleting Event with id {eventId}: {message}", innerException)
        {
        }
    }
}
