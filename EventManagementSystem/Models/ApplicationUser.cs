using Microsoft.AspNetCore.Identity;

namespace Web.Models 
{
    public class ApplicationUser : IdentityUser
    {
      
        public string FullName { get; set; }
       
    }
}
