using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AttendeetDeletionException: Exception
    {
        public AttendeetDeletionException(int attendeeId, string message, Exception innerException)
        : base($"Error deleting attendee with id {attendeeId}: {message}", innerException)
        {
        }
    }
}
