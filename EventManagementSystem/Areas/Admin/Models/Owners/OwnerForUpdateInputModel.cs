using Constracts;
using System.ComponentModel.DataAnnotations;

namespace Web.Areas.Admin.Models.Owners
{
    public class OwnerForUpdateInputModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Họ và tên")]
        public string? Name { get; set; }
        [Display(Name = "Ngày sinh")]
        public DateOnly DateOfBirth { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
    }
}
