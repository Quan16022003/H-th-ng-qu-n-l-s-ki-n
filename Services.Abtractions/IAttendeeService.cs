using Constracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abtractions
{
    public interface IAttendeeService
    {
        Task<IEnumerable<AttendeeDTO>> GetAllAttendeeAsync();

        Task<AttendeeDTO> GetAttendeeByIdAsync(int id);

        Task AddAttendeeAsync(AttendeeDTO attendeeDTO);

        Task UpdateAttendeeAsync(AttendeeDTO attendeeDTO);
        Task UpdateIsCancelledAttendeeAsync(IsCancelledAttendeeDTO isCancelledAttendeeDTO);
        Task UpdateCheckinAttendeeAsync(CheckinAttendeeDTO checkinAttendeeDTO);
        Task DeleteAttendeeAsync(int id);
        
    }
}
