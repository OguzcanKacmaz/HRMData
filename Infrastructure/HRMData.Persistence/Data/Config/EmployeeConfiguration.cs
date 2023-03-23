using HRMData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMData.Persistence.Data.Config
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.Property(x => x.TcIdentificationNumber).HasMaxLength(11);
            builder.HasIndex(x => x.TcIdentificationNumber).IsUnique();
            builder.Property(x => x.FirstName).HasAnnotation("DisplayName", "Ad");
            builder.Property(x => x.MiddleName).HasAnnotation("DisplayName", "İkinci Ad");
            builder.Property(x => x.LastName).HasAnnotation("DisplayName", "Soyad");
            builder.Property(x => x.SecondLastName).HasAnnotation("DisplayName", "İkinci Soyad");
            builder.Property(x => x.Photo).HasAnnotation("DisplayName", "Fotoğraf");
            builder.Property(x => x.DateOfBirth).HasAnnotation("DisplayName", "Doğum Tarihi").HasColumnType("date");
            builder.Property(x => x.PlaceOfBirth).HasAnnotation("DisplayName", "Doğum Yeri");
            builder.Property(x => x.TcIdentificationNumber).HasAnnotation("DisplayName", "TC Kimlik No");
            builder.Property(x => x.HireDate).HasAnnotation("DisplayName", "İşe Giriş Tarihi");
            builder.Property(x => x.TerminationDate).HasAnnotation("DisplayName", "İşten Çıkış Tarihi");
            builder.Property(x => x.IsActive).HasAnnotation("DisplayName", "Aktiflik Durumu");
            builder.Property(x => x.Address).HasAnnotation("DisplayName", "Adres");
            builder.Property(x => x.Salary).HasAnnotation("DisplayName", "Maaş").HasPrecision(18, 2);
            builder.Property(x => x.MaxPaymentAmount).HasAnnotation("DisplayName", "Maksimum Avans Miktarı").HasPrecision(18, 2);
            builder.Property(x => x.RemainingAdvanceAmount).HasAnnotation("DisplayName", "Kalan Avans Miktarı").HasPrecision(18, 2);
            builder.Ignore(x => x.FullName);
        }
    }
}
