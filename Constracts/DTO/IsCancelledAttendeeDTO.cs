using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class IsCancelledAttendeeDTO : BaseDTO
    {
        public bool IsCancelled { get; set; }

        public IsCancelledAttendeeDTO()
        {
        }

        public IsCancelledAttendeeDTO(int id, bool isCancelled)
        {
            Id = id;
            IsCancelled = isCancelled;
        }
    }
}
