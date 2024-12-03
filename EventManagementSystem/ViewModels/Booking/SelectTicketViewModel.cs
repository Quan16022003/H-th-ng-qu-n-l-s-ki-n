using Constracts.DTO;
using System;
using System.Collections.Generic;

namespace Web.ViewModels.Booking
{
    public class SelectTicketViewModel
    {
        public EventDTO eventDTO { get; set; }
        public IEnumerable<TicketDTO> ticketDTOs { get; set; }
        
    }
}