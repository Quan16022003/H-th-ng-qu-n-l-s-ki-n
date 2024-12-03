using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CheckInAttendeeNotFoundException : NotFoundException
    {
        public CheckInAttendeeNotFoundException(int id) : base($" Check in attendee with id {id} not found")
        {
        }
    }
}
