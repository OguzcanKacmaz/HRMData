using HRMData.Domain.Entities.Enums;
using HRMData.Domain.Enums;

namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class EmployeeViewModel
    {
        public string? Photo { get; set; }
        public string? PhotoMini { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? SecondLastName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? TcIdentificationNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public Status IsActive { get; set; }
        public string? TitleName { get; set; }
        public decimal Salary { get; set; }
        public string? Address { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string? DepartmentName { get; set; }
        public string? CompanyName { get; set; }
        public Gender Gender { get; set; }

        public string PhoneNumberFormat(string phonenumber)
        {
            var phoneNumber = phonenumber;
            var formattedPhoneNumber = string.Format("({0}) {1}-{2}",
                phoneNumber[..3],
                phoneNumber.Substring(3, 3),
                phoneNumber[6..]);
            return formattedPhoneNumber;
        }
        public string DateTimeFormat(DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return "-";
            }
            else
            {
                return dateTime.Value.ToString("dd/MM/yyyy");
            }
        }





    }
}
