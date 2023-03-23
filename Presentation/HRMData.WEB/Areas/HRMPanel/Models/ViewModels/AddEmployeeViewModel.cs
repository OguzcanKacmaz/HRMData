using HRMData.Domain.Enums;

namespace HRMData.Domain.Entities
{
    public class AddEmployeeViewModel
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? SecondLastName { get; set; }
        public Gender Gender { get; set; }
        public IFormFile? PhotoPath { get; set; }
        public DateTime DateOfBirth { get; set; }
        public City? PlaceOfBirth { get; set; }
        public string? OtherPlaceOfBirth { get; set; }
        public string? TcIdentificationNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string? TitleId { get; set; }
        public string? DepartmentId { get; set; }
        public string? CompanyId { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Title>? Titles { get; set; }
        public ICollection<Department>? Departments { get; set; }
        public List<Company>? Company { get; set; }

    }
}
