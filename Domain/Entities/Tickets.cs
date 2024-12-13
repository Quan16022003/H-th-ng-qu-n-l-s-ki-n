using Domain.Commons;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tickets : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int MaxPerPerson { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantitySold { get; set; }
        public TicketStatus Status { get; set; }

        public int EventId { get; set; }
        public Events? Event { get; set; }
        public IEnumerable<Attendees>? Attendees { get; set; }
        public IEnumerable<OrderItems>? OrderItems { get; set; }
    }
}
