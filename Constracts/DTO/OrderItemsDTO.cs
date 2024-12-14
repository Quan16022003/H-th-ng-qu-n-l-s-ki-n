using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class OrderItemDTO :BaseDTO  
    {
        public int TicketId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
}
