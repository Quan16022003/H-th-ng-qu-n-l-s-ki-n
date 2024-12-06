using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CheckInAttendeeMismatchedEventIdException: Exception
    {
        public CheckInAttendeeMismatchedEventIdException(int id)
      : base($" Check in attendee {id} has mismatched eventId")
        {
        }
    }
}
