using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TicketUpdateException : Exception
    {
        public TicketUpdateException(int ticketId, string message, Exception innerException)
       : base($"Error updating ticket with id {ticketId}: {message}", innerException)
        {
        }
    }
}
