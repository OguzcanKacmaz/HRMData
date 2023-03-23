using FluentValidation;
using HRMData.WEB.Models.ViewModels;

namespace HRMData.WEB.Models.CustomValidations
{
    public class ForgerPasswordValidation : AbstractValidator<ForgetPasswordViewModel>
    {
        public ForgerPasswordValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Alanı Boş Bırakılamaz")
                .EmailAddress().WithMessage("Email Formatı Yanlış");
        }
    }
}
