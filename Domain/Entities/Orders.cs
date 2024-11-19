using Domain.Commons;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Orders : BaseEntity
    {
        #region Customer
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        #endregion
        public OrderStatus OrderStatus { get; set; }

        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int EventId { get; set; }
        public Events? Event { get; set; }
        public IEnumerable<Attendees>? Attendees { get; set; }
        public IEnumerable<OrderItems>? OrderItems { get; set; }
        public IEnumerable<OrderTicket>? OrderTickets { get; set; }
    }
}
