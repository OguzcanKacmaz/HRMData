using HRMData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRMData.Persistence.Data.Config
{
    internal class DepartmenConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(x => x.Name).HasAnnotation("DisplayName", "Departman Adı");
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
