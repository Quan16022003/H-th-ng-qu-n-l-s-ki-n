using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EventTimingUpdateException : Exception
    {
        public EventTimingUpdateException(int eventId, string message, Exception innerException)
       : base($"Error updating event timing with id {eventId}: {message}", innerException)
        {
        }
    }
}
