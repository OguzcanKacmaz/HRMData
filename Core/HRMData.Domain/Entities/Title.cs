using HRMData.Domain.Entities.Common;

namespace HRMData.Domain.Entities
{
    public class Title : BaseEntity
    {
        public string Name { get; set; } = null!;
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}
