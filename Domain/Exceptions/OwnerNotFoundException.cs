using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class OwnerNotFoundException : NotFoundException
    {
        public OwnerNotFoundException(Guid ownerId)
        : base($"The owner with the identifier {ownerId} was not found.")
        {
        }
    }
}
