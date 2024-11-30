using Constracts.DTO;
using Web.Authorize;

namespace Web.Areas.Dashboard.ViewModels
{
    public class AuthorizedViewModel
    {
        public required UserDTO CurrentUser { get; set; }
        public required AccessPermission Permissions { get; set; }

        public AuthorizedViewModel()
        {

        }
    }
}
