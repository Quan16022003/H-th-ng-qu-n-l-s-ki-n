using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarUrl { get; set; }
        public IEnumerable<Events>? Events { get; set; }
        public IEnumerable<Orders>? Orders { get; set; }
        public IEnumerable<Attendees>? Attendees { get; set; }
    }
}
