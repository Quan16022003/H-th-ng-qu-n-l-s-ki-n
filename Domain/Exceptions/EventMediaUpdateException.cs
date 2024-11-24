using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EventMediaUpdateException : Exception
    {
        public EventMediaUpdateException(int eventId, string message, Exception innerException)
       : base($"Error updating event meida with id {eventId}: {message}", innerException)
        {
        }
    }
}
