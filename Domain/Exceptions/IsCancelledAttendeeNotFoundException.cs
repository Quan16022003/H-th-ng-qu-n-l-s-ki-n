using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class IsCancelledAttendeeNotFoundException: NotFoundException
    {
        public IsCancelledAttendeeNotFoundException(int id) : base($"Attendee is cancelled with id {id} not found")
        {
        }
    }
}
