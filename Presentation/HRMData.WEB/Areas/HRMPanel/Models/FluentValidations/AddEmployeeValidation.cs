using FluentValidation;
using HRMData.Domain.Entities;
using HRMData.Domain.Enums;

namespace HRMData.WEB.Areas.HRMPanel.Models.FluentValidations
{
    public class AddEmployeeValidation : AbstractValidator<AddEmployeeViewModel>
    {
        public AddEmployeeValidation()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim Alanı Boş Olamaz")
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ]*$").WithMessage("İsim Alanı Yalnızca Harflerden oluşmalıdır.")
                .MaximumLength(20).WithMessage("İsim Alanı Maksimum 20 Karakter Olmalıdır")
                .MinimumLength(3).WithMessage("İsim Alanı Minumum 3 Karakter Olmalıdır");

            RuleFor(x => x.MiddleName)
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ]*$").WithMessage("İkinci İsim Alanı Yalnızca Harflerden oluşmalıdır.")
                .MaximumLength(20).WithMessage("İkinci İsim Alanı Maksimum 20 Karakter Olmalıdır")
                .MinimumLength(3).WithMessage("İkinci İsim Alanı Minumum 3 Karakter Olmalıdır");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyisim Alanı Boş Olamaz")
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ]*$").WithMessage("Soyisim Alanı Yalnızca Harflerden oluşmalıdır.")
                .MaximumLength(20).WithMessage("Soyisim Alanı Maksimum 20 Karakter Olmalıdır")
                .MinimumLength(3).WithMessage("Soyisim Alanı Minumum 3 Karakter Olmalıdır");

            RuleFor(x => x.SecondLastName)
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ]*$").WithMessage("İkinci Soyisim Yalnızca Harflerden oluşmalıdır.")
                .MaximumLength(20).WithMessage("İkinci Soyisim Alanı Maksimum 20 Karakter Olmalıdır")
                .MinimumLength(3).WithMessage("İkinci Soyisim Alanı Minumum 3 Karakter Olmalıdır");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email Alanı Boş Olamaz")
                .EmailAddress().WithMessage("Email Adresini Düzgün Formatta Giriniz")
                .Must(x => x.EndsWith("@bilgeadamboost.com")).WithMessage("Mail Adresi @bilgeadamboost.com ile bitmelidir");

            RuleFor(x => x.TcIdentificationNumber)
                .NotEmpty().WithMessage("TC Kimlik No Alanı Boş Olamaz")
                .Length(11).WithMessage("TC Kimlik No Alanı 11 Haneli Olmak Zorundadır")
                .Matches("^[0-9]*$").WithMessage("TC Kimlik No Alanı Sadece Sayı İçermelidir");

            RuleFor(x => x.PlaceOfBirth)
                .NotEqual(City.None).WithMessage("Lütfen Geçerli Bir Doğum Yeri Seçiniz.")
                .IsInEnum().WithMessage("Doğum Yeri Alanı Seçimi Geçersiz");

            RuleFor(person => person.OtherPlaceOfBirth)
                .NotEmpty().When(person => person.PlaceOfBirth == City.Diğer)
                .WithMessage("Diğer Doğum Yeri Alanı boş bırakılamaz");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Doğum Tarihi Boş Olamaz")
                .Must(date => IsAgeBetween(date, 18, 65))
                .WithMessage("Yaş 18-65 Aralığında Olmalıdır.");

            RuleFor(x => x.HireDate)
                .NotEmpty().WithMessage("İşe Giriş Tarihi Alanı Boş Olamaz")
                .Must(date => date >= DateTime.Today.AddYears(-20))
                .WithMessage("En Fazla 20 yıl Geriye Dönük Giriş Yapılabilir")
                .Must(date => date <= DateTime.Today.AddMonths(1))
                .WithMessage("En Fazla 1 Ay ileriye Dönük Giriş Yapılabilir");

            RuleFor(x => x.TerminationDate)
                .Cascade(CascadeMode.Continue)
                .Must((model, terminationDate) => terminationDate == null || terminationDate > model.HireDate)
                .WithMessage("İşten Ayrılış Tarihi, İşe Giriş Tarihinden Büyük Olmalıdır.")
                .Must((model, terminationDate) => terminationDate == null || terminationDate <= DateTime.Now)
                .WithMessage("İşten Ayrılış Tarihi, Şuanki Zamandan Küçük veya Eşit Olmalıdır.")
                .When(x => x.TerminationDate != null);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon Alanı Boş Olamaz")
                .Matches(@"^5\d{9,}$").WithMessage("Telefon Numarsının Başında 0 Olmadan Giriniz Ve Telefon Numaranız 5 ile başlamalıdır")
                .Length(10).WithMessage("Telefon numarası 10 karakter uzunluğunda olmalıdır");

            RuleFor(x => x.Salary)
                .NotEmpty().WithMessage("Maaş Alanı Boş Olamaz")
                .InclusiveBetween(8506, 100000).WithMessage("Maaş 8506-100000 Aralığında Olmalıdır");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Cinsiyet Alanı Seçimi Geçersiz");

            RuleFor(x => x.TitleId)
                .NotNull().WithMessage("Meslek Alanı Seçimi Boş Olamaz")
                .Must(x => x != 0.ToString()).WithMessage("Meslek Alanı Seçimi Boş Olamaz");

            RuleFor(x => x.CompanyId)
               .NotNull().WithMessage("Şirket Alanı Seçimi Boş Olamaz")
               .Must(x => x != 0.ToString()).WithMessage("Şirket Alanı Seçimi Boş Olamaz");

            RuleFor(x => x.DepartmentId)
               .NotNull().WithMessage("Departman Alanı Seçimi Boş Olamaz")
               .Must(x => x != 0.ToString()).WithMessage("Departman Alanı Seçimi Boş Olamaz");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres Alanı Boş Olamaz")
                .MinimumLength(20).WithMessage("Adres Alanı Minumum 20 Karakter Uzunluğunda Olmalıdır");

            RuleFor(x => x.OtherPlaceOfBirth)
                .Matches("^[a-zA-ZçÇğĞıİöÖşŞüÜ]*$").WithMessage("Doğum Yeri Sadece Harflerden Oluşmalıdır");

        }
        private static bool IsAgeBetween(DateTime birthDate, int minAge, int maxAge)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;

            return age >= minAge && age <= maxAge;
        }
    }
}
