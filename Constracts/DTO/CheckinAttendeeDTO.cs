using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class CheckinAttendeeDTO: BaseDTO
    {
        public bool HasArrived { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public int EventId { get; set; }

        public CheckinAttendeeDTO()
        {
        }

        public CheckinAttendeeDTO(int id, bool hasArrived, DateTime? arrivalTime, int eventId)
        {
            Id = id;
            HasArrived = hasArrived;
            ArrivalTime = arrivalTime;
            EventId = eventId;
        }
    }
}
