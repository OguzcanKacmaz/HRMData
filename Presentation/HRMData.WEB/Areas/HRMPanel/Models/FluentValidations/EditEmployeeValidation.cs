using FluentValidation;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;

namespace HRMData.WEB.Areas.HRMPanel.Models.FluentValidations
{
    public class EditEmployeeValidation : AbstractValidator<EditEmployeeViewModel>
    {
        public EditEmployeeValidation()
        {
            RuleFor(employee => employee.PhoneNumber)
            .Matches(@"^5\d{9,}$")
            .WithMessage("Lütfen telefon numaranızı başında 0 olmadan 10 haneli sadece rakam olarak giriniz")
            .Length(10)
            .WithMessage("Telefon numarası 10 karakter uzunluğunda olmalıdır")
            .NotEmpty()
            .WithMessage("Telefon Alanı Boş Olamaz");


            RuleFor(x => x.PhotoPath)
                    .Must(BeAValidFileExtension!).WithMessage("Geçersiz dosya uzantısı")
                    .Must(BeAValidContentType!).WithMessage("Geçersiz dosya türü");

            RuleFor(x => x.Address)
                .NotNull()
                .WithMessage("Adres Alanı Boş Olamaz");
            RuleFor(x => x.Address)
                .MinimumLength(20).WithMessage("Adres Alanı En Az 20 Karakter Uzunluğunda Olmalıdır");
        }
        private bool BeAValidFileExtension(IFormFile file)
        {

            if (file == null) return true;
            var allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(fileExtension.ToLower());
        }

        private bool BeAValidContentType(IFormFile file)
        {
            if (file == null) return true;
            return file.ContentType.ToLower().StartsWith("image/");
        }


    }
}

