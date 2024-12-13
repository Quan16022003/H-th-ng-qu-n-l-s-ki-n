using Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    // Đang bị lỗi với chưa cần thiết.
    public class Attendees : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsCancelled { get; set; }
        public bool HasArrived { get; set; }
        public DateTime? ArrivalTime { get; set; }
        
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int OrderId { get; set; }
        public Orders? Order { get; set; }
        public int TicketId { get; set; }
        public Tickets? Ticket { get; set; }
        public int EventId { get; set; }
        public Events? Event { get; set; }
    }
}
