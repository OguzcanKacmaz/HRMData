using HRMData.Domain.Entities.Common;
using HRMData.Domain.Entities.Enums;
using HRMData.Domain.Enums;

namespace HRMData.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string MersisNo { get; set; }=null!;
        public string TaxNumber { get; set; } = null!;
        public string TaxAdministration { get; set; } = null!;
        public string LogoMini { get; set; } = null!;
        public string Logo { get; set; } = null!;
        public CompanyTitle CompanyTitle { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int EmployeeCount { get; set; }
        public int FoundationYear { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public Status IsActive { get; set; }

        public ICollection<Employee> Employees { get; set; } = null!;
        public ICollection<Department> Departments { get; set; } = null!;
    }
}
