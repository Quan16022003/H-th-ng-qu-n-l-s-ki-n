using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constracts.DTO
{
    public class UserDTO
    {
        public string? Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        public string? PasswordHash { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Role { get; set; }

        public bool IsLockout { get; set; }
        public DateTime LockoutExpiredDate { get; set; }
        public int AccessCount { get; set; }
    }
}
