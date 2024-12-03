using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AttendeeNotFoundException: NotFoundException
    {
        public AttendeeNotFoundException(int id) : base($"Attendee with id {id} not found")
        {
        }
    }
}
