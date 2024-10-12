using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.Models.Owners
{
    public class OwnerListViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateOnly DateOfBirth { get; set; }
    }
}
