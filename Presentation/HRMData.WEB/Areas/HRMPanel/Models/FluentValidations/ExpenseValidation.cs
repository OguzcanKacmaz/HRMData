using FluentValidation;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;

namespace HRMData.WEB.Areas.HRMPanel.Models.FluentValidations
{
    public class ExpenseValidation : AbstractValidator<AddEmployeeExpenseViewModel>
    {

        public ExpenseValidation()
        {
            RuleFor(x => x.ExpenseDocumentPath)
                .Must(BeAValidFileExtension!).WithMessage("Geçersiz Dosya Uzantısı")
                .Must(BeAValidContentType!).WithMessage("Geçersiz Dosya Türü")
                .NotNull().WithMessage("Dosya Alanı Boş Olamaz");

            RuleFor(x => x.Currency)
                .IsInEnum().WithMessage("Harcama Talebi Para Birimi Alanı Seçimi Geçersiz")
                .NotNull().WithMessage("Harcama Talebi Para Birim Alanı Boş Olamaz");

            RuleFor(x => x.ExpenseType)
                .IsInEnum().WithMessage("Avans Türü Alanı Seçimi Geçersiz")
                .NotNull().WithMessage("Avans Türü  Alanı Boş Olamaz");

            RuleFor(x => x.Amount).NotNull().WithMessage("Harcama Talebi Tutar Alanı Boş Olamaz").InclusiveBetween(100, 5000).WithMessage("Talep Edilecek Tutar 100-5000 Arasında Olmalıdır");

        }
        private bool BeAValidFileExtension(IFormFile file)
        {

            if (file == null) return true;
            var allowedExtensions = new[] { ".jpeg", ".jpg", ".png", ".pdf" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(fileExtension.ToLower());
        }
        private bool BeAValidContentType(IFormFile file)
        {
            if (file == null) return true;
            if (file.ContentType.ToLower().StartsWith("image/") || file.ContentType.ToLower().StartsWith("application/"))
                return true;
            return false;
        }
    }
}
