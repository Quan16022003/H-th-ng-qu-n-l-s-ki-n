using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public abstract class BaseDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
