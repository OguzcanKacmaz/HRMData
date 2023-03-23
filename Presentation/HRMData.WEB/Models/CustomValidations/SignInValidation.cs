using FluentValidation;
using HRMData.WEB.Models.ViewModels;

namespace HRMData.WEB.Models.CustomValidations
{
    public class UserValidator : AbstractValidator<SignInViewModel>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("Email alanı boş geçilemez.")
                .EmailAddress().WithMessage("Lütfen geçerli bir email adresi giriniz.");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Şifre Alanı Boş Olamaz");

        }

    }
}
