using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts
{
    public class OwnerForCreationDto
    {
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
