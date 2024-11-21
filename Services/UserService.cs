using Constracts.DTO;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal sealed class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = unitOfWork.UserManager;
        }

        public async Task AddUserAsync(UserDTO userDTO)
        {
            ArgumentNullException.ThrowIfNull(userDTO);

            var user = userDTO.Adapt<ApplicationUser>();

            await _userManager.CreateAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            ArgumentNullException.ThrowIfNull(userDTO);

            var user = userDTO.Adapt<ApplicationUser>();

            await _userManager.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteUserAsync(UserDTO userDTO)
        {
            ArgumentNullException.ThrowIfNull(userDTO);

            var user = userDTO.Adapt<ApplicationUser>();

            await _userManager.DeleteAsync(user);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<UserDTO> GetCurrentUserAsync(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            ArgumentNullException.ThrowIfNull(user);

            var result = user.Adapt<UserDTO>();
            result.Role = string.Join(", ", [.. _userManager.GetRolesAsync(user).Result]);

            return result;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var result = users.Select(u =>
            {
                UserDTO dto = u.Adapt<UserDTO>();
                dto.Role = string.Join(", ", [.._userManager.GetRolesAsync(u).Result]);

                return dto;
            });

            return result;
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            ArgumentNullException.ThrowIfNull(user);

            var result = user.Adapt<UserDTO>();
            result.Role = string.Join(", ", [.._userManager.GetRolesAsync(user).Result]);

            return result;
        }
    }
}
