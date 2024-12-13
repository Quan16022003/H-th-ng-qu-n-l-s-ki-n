using Constracts.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abtractions
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrlAsync(PaymentInfomationDTO model, HttpContext context);
        Task<PaymentResponseDTO> PaymentExecuteAsync(IQueryCollection collections);
    }
}
