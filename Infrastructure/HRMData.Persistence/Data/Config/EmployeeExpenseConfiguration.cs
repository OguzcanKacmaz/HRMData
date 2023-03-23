using HRMData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMData.Persistence.Data.Config
{
    public class EmployeeExpenseConfiguration : IEntityTypeConfiguration<EmployeeExpense>
    {
        public void Configure(EntityTypeBuilder<EmployeeExpense> builder)
        {
            builder.Property(x => x.Currency).HasAnnotation("DisplayName", "Para Birimi");
            builder.Property(x => x.ApprovalStatus).HasAnnotation("DisplayName", "Onay Durumu");
            builder.Property(x => x.Amount).HasAnnotation("DisplayName", "Harcama Tutarı").HasPrecision(18, 2);
            builder.Property(x => x.ExpenseType).HasAnnotation("DisplayName", "Harcama Türü");
            builder.Property(x => x.ReplyDate).HasAnnotation("DisplayName", "Cevaplanma Tarihi");
            builder.Property(x => x.RequestDate).HasAnnotation("DisplayName", "Harcama Tarihi");
            builder.Property(x => x.ExpenseDocument).HasAnnotation("DisplayName", "Harcama Dökümanı");
        }
    }
}
