using HRMData.Domain.Entities.Enums;
using HRMData.Domain.Enums;

namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class CompanyViewModel
    {
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public string? LogoMini { get; set; }
        public CompanyTitle CompanyTitle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }       
        public string? MersisNo { get; set; }
        public string? TaxNumber { get; set; }
        public string? TaxAdministration { get; set; }    
        public int EmployeeCount { get; set; }
        public int FoundationYear { get; set; }
        public Status IsActive { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string PhoneNumberFormat(string phoneNumber)
        {
            var formattedPhoneNumber = "";

            if (phoneNumber.Length == 7) // 7 haneli telefon numarası için
            {
                formattedPhoneNumber = string.Format("({0})-{1}-{2}",
                    phoneNumber[..3],
                    phoneNumber.Substring(3, 2),
                    phoneNumber[5..]);
            }
            else // 10 haneli telefon numarası için
            {
                formattedPhoneNumber = string.Format("({0})-{1}-{2}-{3}",
                    phoneNumber[..3],
                    phoneNumber.Substring(3, 3),
                    phoneNumber.Substring(6, 2),
                    phoneNumber[8..]);
            }

            return formattedPhoneNumber;
        }
    }
}
