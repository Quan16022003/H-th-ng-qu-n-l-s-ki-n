using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CheckInAttendeeUpdateException : Exception
    {
        public CheckInAttendeeUpdateException(int attendeeId, string message, Exception innerException)
      : base($"Error updating check in attendee with id {attendeeId}: {message}", innerException)
        {
        }
    }
}
