using HRMData.Domain.Entities.Common;
using HRMData.Domain.Entities.Enums;
using HRMData.Domain.Enums;

namespace HRMData.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? SecondLastName { get; set; }
        public string? FullName
        {
            get
            {
                var fullName = "";
                if (MiddleName == null && SecondLastName == null)
                    fullName = FirstName + " " + LastName;
                else if (MiddleName != null && SecondLastName == null)
                    fullName = FirstName + " " + MiddleName + " " + LastName;
                else if (MiddleName != null && SecondLastName != null)
                    fullName = FirstName + " " + MiddleName + " " + LastName + " " + SecondLastName;
                return fullName;
            }
        }
        public Gender Gender { get; set; }
        public double AnnualLeaveDays { get; set; } = 0;
        public double RemainingAnnualLeaveDays { get; set; } = 0;
        public double ExcusedAbsenceDay { get; set; } = 1;
        public DateTime ResetDate { get; set; }
        public string? Photo { get; set; }
        public string? PhotoMini { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; } = null!;
        public string TcIdentificationNumber { get; set; } = null!;
        public DateTime HireDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public Status IsActive { get; set; }

        public string Address { get; set; } = null!;
        public bool RandomPassword { get; set; } = true;
        public decimal Salary { get; set; }
        public decimal MaxPaymentAmount { get; set; }
        public decimal RemainingAdvanceAmount { get; set; }
        public string? DepartmentId { get; set; }
        public string? TitleId { get; set; }
        public Department? Department { get; set; }
        public string AppUserId { get; set; } = null!;
        public virtual AppUser AppUser { get; set; } = null!;
        public string? CompanyId { get; set; }
        public Company? Company { get; set; }
        public List<EmployeeAdvance>? Advances { get; set; }
        public List<EmployeeExpense>? Expense { get; set; }
        public List<EmployeePermissionRequest>? PermissionRequest { get; set; }
    }
}
