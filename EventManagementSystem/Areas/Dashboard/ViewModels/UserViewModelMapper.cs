using Domain.Entities;

namespace Web.Areas.Dashboard.ViewModels
{
    public static class UserViewModelMapper
    {
        public static UserViewModel Map(ApplicationUser user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                Password = user.PasswordHash,
                Email = user.Email,
                Phone = user.PhoneNumber,
                IsLockout = user.LockoutEnabled,
                LockoutExpiredDay = user.LockoutEnd,
                AccessFailedTime = user.AccessFailedCount,
            };
        }

    }
}
