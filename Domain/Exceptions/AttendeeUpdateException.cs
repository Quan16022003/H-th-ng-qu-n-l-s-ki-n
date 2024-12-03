using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AttendeeUpdateException: Exception
    {
        public AttendeeUpdateException(int attendeeId, string message, Exception innerException)
      : base($"Error updating attendee with id {attendeeId}: {message}", innerException)
        {
        }
    }
}
