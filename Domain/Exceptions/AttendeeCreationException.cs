using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AttendeeCreationException : Exception
    {
        public AttendeeCreationException(string message, Exception innerException)
        : base($"Error creating Attendee: {message}", innerException)
        {
        }
    }
}
