using HRMData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMData.Persistence.Data.Config
{
    public class TitleConfiguration : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.Property(x => x.Name).HasAnnotation("DisplayName", "Meslek Adı");
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Employees).WithOne().HasForeignKey(x => x.TitleId);

        }
    }
}
