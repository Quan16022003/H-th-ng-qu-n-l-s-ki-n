using Constracts.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abtractions
{
    /// <summary>
    /// User Manager with UserDTO
    /// </summary>
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        Task<UserDTO> GetUserByIdAsync(string id);

        Task AddUserAsync(UserDTO user);

        Task UpdateUserAsync(UserDTO user);


        Task DeleteUserAsync(UserDTO user);
    }
}