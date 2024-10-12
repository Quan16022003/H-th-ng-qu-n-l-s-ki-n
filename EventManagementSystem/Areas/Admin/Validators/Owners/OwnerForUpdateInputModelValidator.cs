using FluentValidation;
using Web.Areas.Admin.Models.Owners;
namespace Web.Areas.Admin.Validators.Owners
{
    public class OwnerForUpdateInputModelValidator : AbstractValidator<OwnerForUpdateInputModel>
    {
        public OwnerForUpdateInputModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên là bắt buộc.")
                .Length(1, 100).WithMessage("Tên không được dài quá 100 ký tự.")
                .Matches(@"^([A-Z][a-zàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđĐ]+\s?)+$")
                    .WithMessage("Tên chỉ được chứa chữ cái và khoảng trắng.");


            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Ngày sinh là bắt buộc.")
                .Must(BeAValidDateOfBirth).WithMessage("Ngày sinh không thể ở tương lai.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Địa chỉ là bắt buộc.")
                .Length(1, 200).WithMessage("Địa chỉ không được dài quá 200 ký tự.");
        }

        private bool BeAValidDateOfBirth(DateOnly dateOfBirth)
        {
            return dateOfBirth <= DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
