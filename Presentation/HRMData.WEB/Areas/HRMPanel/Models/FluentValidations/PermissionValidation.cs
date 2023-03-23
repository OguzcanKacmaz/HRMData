using FluentValidation;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;
namespace HRMData.WEB.Areas.HRMPanel.Models.FluentValidations
{
    public class PermissionValidation : AbstractValidator<AddEmployeePermissionRequestViewModel>
    {
        public PermissionValidation()
        {
            RuleFor(x => x.PermissionType)
            .IsInEnum().WithMessage("İzin Türü Alanı Seçimi Geçersiz")
            .NotNull().WithMessage("İzin Türü Alanı Boş Olamaz");

            RuleFor(x => x.EndDate)
    .NotEmpty().WithMessage("Lütfen Bir Tarih Seçin")
    .Must(date => date > DateTime.Now).WithMessage("Geçmiş Zamanlı Tarih Giremezsiniz")
    .Must((model, endDate) => (endDate - model.StartDate).Days <= 90 || model.PermissionType != Domain.Enums.PermissionType.UnpaidLeave)
        .WithMessage("Ücretsiz izin maksimum 3 ay seçilebilir.")
    .GreaterThan(x => x.StartDate).WithMessage("Girilen Tarih İzin Başlangıç Tarihinden Önce Olamaz");

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Lütfen Bir Tarih Seçin")
            .Must(date => date >= DateTime.Today).WithMessage("Geçmiş Zamanlı Tarih Giremezsiniz");

            RuleFor(x => x.StartDate)
           .Must(date => date >= DateTime.Now.Date.AddDays(1) && date <= DateTime.Now.Date.AddDays(5))
           .When(x => x.PermissionType.HasValue && x.PermissionType.Value == Domain.Enums.PermissionType.BereavementLeave) // sadece vefat izni seçilmişse
           .WithMessage("İzin Talebini 5 Gün İleri Tarihli Giremezsiniz");


            RuleFor(x => x.NumOfDays)
                .NotEmpty().WithMessage("İzin Gün Sayısı Alanı Boş Olamaz")
                .GreaterThan(0).WithMessage("Gün Sayısı 0 veya 0'dan Küçük Olamaz");

            RuleFor(x => x.DocumentPath)
                .Must(BeAValidFileExtension!)
                .When
                (x => x.PermissionType == Domain.Enums.PermissionType.PaternityLeave ||
                x.PermissionType == Domain.Enums.PermissionType.BereavementLeave ||
                x.PermissionType == Domain.Enums.PermissionType.PostnatalLeave ||
                x.PermissionType == Domain.Enums.PermissionType.AntenatalLeave ||
                x.PermissionType == Domain.Enums.PermissionType.MarriageLeave ||
                x.PermissionType == Domain.Enums.PermissionType.SickLeave)
                .WithMessage("Geçersiz Dosya Uzantısı")
                .Must(BeAValidContentType!)
                .When
                (x => x.PermissionType == Domain.Enums.PermissionType.PaternityLeave ||
                x.PermissionType == Domain.Enums.PermissionType.BereavementLeave ||
                x.PermissionType == Domain.Enums.PermissionType.PostnatalLeave ||
                x.PermissionType == Domain.Enums.PermissionType.AntenatalLeave ||
                x.PermissionType == Domain.Enums.PermissionType.MarriageLeave ||
                x.PermissionType == Domain.Enums.PermissionType.SickLeave)
                .WithMessage("Geçersiz Dosya Türü")
                .NotNull()
                .When
                (x => x.PermissionType == Domain.Enums.PermissionType.PaternityLeave ||
                x.PermissionType == Domain.Enums.PermissionType.BereavementLeave ||
                x.PermissionType == Domain.Enums.PermissionType.PostnatalLeave ||
                x.PermissionType == Domain.Enums.PermissionType.AntenatalLeave ||
                x.PermissionType == Domain.Enums.PermissionType.MarriageLeave ||
                x.PermissionType == Domain.Enums.PermissionType.SickLeave)
                .WithMessage("Dosya Alanı Boş Olamaz");
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