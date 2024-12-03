using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class AttendeeDTO: BaseDTO

    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsCancelled { get; set; }
        public bool HasArrived { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string UserId { get; set; }
        public int OrderId { get; set; }
        public int TicketId { get; set; }
        public int EventId { get; set; }
    }
}
