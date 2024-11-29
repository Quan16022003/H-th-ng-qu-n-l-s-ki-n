using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItems : BaseEntity
    {
        public int OrderId { get; set; }
        public Orders? Order { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
}
