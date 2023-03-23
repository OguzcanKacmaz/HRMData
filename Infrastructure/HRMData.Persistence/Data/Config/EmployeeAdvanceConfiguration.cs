using HRMData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMData.Persistence.Data.Config
{
    internal class EmployeeAdvanceConfiguration : IEntityTypeConfiguration<EmployeeAdvance>
    {
        public void Configure(EntityTypeBuilder<EmployeeAdvance> builder)
        {
            builder.Property(x => x.Currency).HasAnnotation("DisplayName", "Para Birimi");
            builder.Property(x => x.ApprovalStatus).HasAnnotation("DisplayName", "Onay Durumu");
            builder.Property(x => x.Amount).HasAnnotation("DisplayName", "Avans Tutarı").HasPrecision(18, 2);
            builder.Property(x => x.Description).HasAnnotation("DisplayName", "Açıklama");
            builder.Property(x => x.ReplyDate).HasAnnotation("DisplayName", "Cevaplanma Tarihi");
            builder.Property(x => x.RequestDate).HasAnnotation("DisplayName", "Avans Tarihi");
            builder.Property(x => x.AdvancePaymentType).HasAnnotation("DisplayName", "Avans Türü");
        }
    }
}
