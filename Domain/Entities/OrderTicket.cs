using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderTicket : BaseEntity
    {
        public int OrderId { get; set; }
        public Orders Order { get; set; }
        public int TicketId { get; set; }
        public Tickets Ticket { get; set; }
    }
}
