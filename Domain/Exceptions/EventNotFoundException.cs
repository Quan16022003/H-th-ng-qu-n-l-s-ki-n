using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EventNotFoundException: NotFoundException
    {
        public EventNotFoundException(int id) : base($"Event with id {id} not found")
        {
        }
    }
}
