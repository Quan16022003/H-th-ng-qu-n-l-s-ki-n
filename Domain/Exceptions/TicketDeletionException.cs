using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TicketDeletionException : Exception
    {
        public TicketDeletionException(int ticketId, string message, Exception innerException)
        : base($"Error deleting ticket with id {ticketId}: {message}", innerException)
        {
        }
    }
}
