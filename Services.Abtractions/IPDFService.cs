using Constracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abtractions
{
    public interface IPDFService
    {
        Task<string> GenerateTicketPdfAsync(AttendeePDFDTO attendeePDFDTO);
    }
}
