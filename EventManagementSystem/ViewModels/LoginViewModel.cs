using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự và tối đa {1} ký tự")]
        public string Password { get; set; }
        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}