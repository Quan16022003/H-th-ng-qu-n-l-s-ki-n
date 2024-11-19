namespace Web.Areas.Dashboard.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Role { get; set; }

        public bool IsLockout { get; set; }

        public DateTimeOffset? LockoutExpiredDay { get; set; }
        public int AccessFailedTime { get; set; }

        public UserViewModel()
        {

        }
    }
}
