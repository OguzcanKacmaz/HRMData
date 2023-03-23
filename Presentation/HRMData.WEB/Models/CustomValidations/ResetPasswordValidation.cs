using FluentValidation;
using HRMData.WEB.Models.ViewModels;

namespace HRMData.WEB.Models.CustomValidations
{
    public class ResetPasswordValidation : AbstractValidator<ResetPasswordViewModel>
    {
        public ResetPasswordValidation()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Yeni Şifre Alanı Boş Olamaz");
            RuleFor(x => x.PasswordConfirm).NotEmpty().WithMessage("Yeni Şifre Tekrar Alanı Boş Olamaz")
                .Equal(x => x.Password).WithMessage("Şifreler Uyuşmuyor");

        }
    }
}
