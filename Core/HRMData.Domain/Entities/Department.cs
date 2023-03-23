using HRMData.Domain.Entities.Common;

namespace HRMData.Domain.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string CompanyId { get; set; } = null!;
        public Company Company { get; set; } = null!;
        public ICollection<Employee> Employees { get; set; } = null!;
    }
}
