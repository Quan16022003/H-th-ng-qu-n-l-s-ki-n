using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class TicketDTO: BaseDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int MaxPerPerson { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantitySold { get; set; }
        public int EventId { get; set; }
        public TicketStatus Status { get; set; }
    }
}
