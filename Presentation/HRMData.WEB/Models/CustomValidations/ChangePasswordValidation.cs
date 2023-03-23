using FluentValidation;
using HRMData.WEB.Models.ViewModels;

namespace HRMData.WEB.Models.CustomValidations
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordValidation()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Geçici Şifre Boş Olamaz");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Yeni Şifre Alanı Boş Olamaz");
            RuleFor(x => x.NewPasswordRepeat).NotEmpty().WithMessage("Yeni Şifre Tekrar Alanı Boş Olamaz")
                .Equal(x => x.NewPassword).WithMessage("Şifreler Uyuşmuyor");
        }
    }
}
