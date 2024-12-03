using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class IsCancelledAttendeeUpdateException: Exception
    {
        public IsCancelledAttendeeUpdateException(int attendeeId, string message, Exception innerException)
      : base($"Error updating attendee is cancelled with id {attendeeId}: {message}", innerException)
        {
        }
    }
}
