using FluentValidation;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;

namespace HRMData.WEB.Areas.HRMPanel.Models.FluentValidations
{
    public class AdvanceValidation : AbstractValidator<AddEmployeeAdvanceViewModel>
    {

        public AdvanceValidation()
        {
            RuleFor(x => x.Currency)
                .IsInEnum().WithMessage("Avans Talep Para Birimi Alanı Seçimi Geçersiz")
                .NotNull().WithMessage("Avans Talep Para Birim Alanı Boş Olamaz");


            RuleFor(x => x.Description)
                .NotNull().WithMessage("Avans Talep Açıklama Alanı Boş Olamaz");

            RuleFor(x => x.AdvancePaymentType)
                .IsInEnum().WithMessage("Avans Türü Alanı Seçimi Geçersiz")
                .NotNull().WithMessage("Avans Türü  Alanı Boş Olamaz");
            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage("Avans Talep Tutarı Alanı Boş Olamaz");

            RuleFor(x => x.Amount)
                .InclusiveBetween(1000, 20000)
                .When(x => x.AdvancePaymentType == Domain.Enums.AdvancePaymentType.Individual)
                .WithMessage("Talep edilecek tutar Tek Seferde 1000-20000 Arasında olmalıdır");
        }
    }

}

