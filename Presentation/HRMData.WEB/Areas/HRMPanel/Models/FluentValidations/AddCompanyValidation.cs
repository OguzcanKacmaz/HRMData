using FluentValidation;
using HRMData.Domain.Enums;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;
using Microsoft.VisualBasic;

namespace HRMData.WEB.Areas.HRMPanel.Models.FluentValidations
{
    public class AddCompanyValidation : AbstractValidator<AddCompanyViewModel>
    {

        public AddCompanyValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Şirket İsim Alanı Boş Olamaz")
                 .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]*$").WithMessage("Şirket İsim Alanı Yalnızca Harflerden Oluşmalıdır.")
                .MaximumLength(50).WithMessage("Şirket İsim Alanı Maksimum 50 Karakter Olmalıdır")
                .MinimumLength(3).WithMessage("Şirket İsim Alanı Minumum 3 Karakter Olmalıdır");
            RuleFor(x => x.TaxAdministration)
                  .NotEmpty().WithMessage("Vergi Dairesi Alanı Boş Olamaz")
                    .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ\\s]*$").WithMessage("Vergi Dairesi Alanı Yalnızca Harflerden Oluşmalıdır.");
            RuleFor(x => x.TaxNumber)
                .NotEmpty().WithMessage("Vergi Numarası Alanı Boş Olamaz.")
                .Matches("^[0-9]+$").WithMessage("Vergi Numarası Sadece Sayılardan Oluşmalıdır")
                .Length(10).WithMessage("Vergi Numarası 10 Karakter Uzunluğunda Olmalıdır.")
                .Must(x =>
                {
                    if (x != null && x.Length == 10)
                    {
                        var v = new int[9];
                        var lastDigit = int.Parse(x[9].ToString());
                        for (var i = 0; i < 9; i++)
                        {
                            var tmp = (int.Parse(x[i].ToString()) + (9 - i)) % 10;
                            v[i] = (tmp * (int)Math.Pow(2, (9 - i))) % 9;
                            if (tmp != 0 && v[i] == 0)
                            {
                                v[i] = 9;
                            }
                        }
                        var sum = v.Sum() % 10;
                        return (10 - (sum % 10)) % 10 == lastDigit;
                    }
                    return false;
                }).WithMessage("Geçersiz Vergi Numarası");

            RuleFor(x => x.MersisNo)
             .NotEmpty().WithMessage("Mersis Numarası Alanı Boş Olamaz")
              .Matches("^[0-9]+$").WithMessage("Mersis Numarası Sadece Sayılardan Oluşmalıdır")
             .Length(16).WithMessage("Mersis Numarası Alanı 16 karakter uzunluğunda olmalıdır.")
             .Must((model, vn) =>
             {
                 if (vn != null && vn.Length == 16)
                 {
                     if (vn.StartsWith("0") && (vn.EndsWith("00015") || vn.EndsWith("00016") || vn.EndsWith("00017")))
                     {
                         var vergiNo = vn.Substring(1, 10);
                         if (Int64.TryParse(vergiNo, out long vnNumeric))
                         {
                             return vnNumeric.ToString() == model.TaxNumber;
                         }
                     }
                 }
                 return false;
             }).WithMessage("Geçersiz Mersis Numarası");

            RuleFor(x => x.FoundationYear)
              .NotEmpty().WithMessage("Kuruluş Yılı Alanı Boş Olamaz")
              .Must(x => x <= DateAndTime.Now.Year).WithMessage($"Şirket Kuruluş Yılı {DateTime.Now.Year} Yılından Küçük Veya Eşit Olmak Zorundadır");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon Alanı Boş Olamaz")
                .MinimumLength(7).WithMessage("Telefon numarası Minimum 7 karakter uzunluğunda olmalıdır")
                .Matches(@"^(?!0)[0-9]{6,14}$").WithMessage("Lütfen Geçerli Bir Telefon Numarası Girin ve Numaranın Başında Sıfır Olmadığından Emin Olun.");

            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Email Alanı Boş Olamaz")
              .Must(x => x!.StartsWith("info@")).WithMessage("Mail Adresi info@ İle Başlamalıdır")
              .EmailAddress().WithMessage("Email Adresi Doğru Formatta Giriniz");

            RuleFor(x => x.EmployeeCount)
              .NotEmpty().WithMessage("Çalışan Sayısı Alanı Boş Olamaz")
              .GreaterThanOrEqualTo(1).WithMessage("Çalışan Sayısı En Az 1 Olmalıdır");

            RuleFor(x => x.ContractStartDate)
              .NotEmpty().WithMessage("Sözleşme Başlangıç Tarihi Alanı Boş Olamaz")
              .Must(date => date >= DateTime.Today.AddYears(-20)).WithMessage("Sözleşme Başlangıç Tarihi 20 Yıldan Fazla Geriye Gidemez")
              .Must(date => date <= DateTime.Today.AddYears(2)).WithMessage("İleriye Dönük Sözleşme Başlangıç Tarihi En Fazla 2 Yıl Girilebilir");

            RuleFor(x => x.ContractEndDate)
               .NotEmpty().WithMessage("Sözleşme Bitiş Tarihi Alanı Boş Olamaz")
               .Must((contract, endDate) => endDate >= contract.ContractStartDate)
                    .WithMessage("Sözleşme Bitiş Tarihi, Sözleşme Başlangıç Tarihinden Önce Olamaz")
               .Must(date => date > DateTime.Now && date < DateTime.Now.AddYears(50))
                    .WithMessage($"Sözleşme Bitiş Tarihi {DateAndTime.Now.ToShortDateString()} - {DateTime.Now.AddYears(50).ToShortDateString()} Tarih Aralığında Olmalıdır");

            RuleFor(x => x.CompanyTitle)
                .NotEqual(CompanyTitle.None).WithMessage("Lütfen Geçerli Bir Ünvan Seçiniz.")
                .IsInEnum().WithMessage("Şirket Ünvan Alanı Boş Olamaz");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres Alanı Boş Olamaz")
                .MinimumLength(20).WithMessage("Adres Alanı Minumum 20 Karakter Uzunluğunda Olmalıdır");

            RuleFor(x => x.LogoPath)
                .Must(BeAValidFileExtension!).WithMessage("Geçersiz Dosya Uzantısı")
                .Must(BeAValidContentType!).WithMessage("Geçersiz Dosya Türü")
                .NotEmpty().WithMessage("Logo Alanı Boş Olamaz");


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
