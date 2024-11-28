using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TicketNotFoundException: NotFoundException
    {
        public TicketNotFoundException(int id) : base($"Ticket with id {id} not found")
        {
        }
    }
}
