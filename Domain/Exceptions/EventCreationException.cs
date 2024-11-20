using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EventCreationException: Exception
    {
        public EventCreationException(string message, Exception innerException)
        : base($"Error creating Event: {message}", innerException)
        {
        }
    }
}
