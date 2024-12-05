using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EventBySlugNotFoundException : NotFoundException
    {
        public EventBySlugNotFoundException(String slug) : base($"Event with id {slug} not found")
        {
        }
    }
}
