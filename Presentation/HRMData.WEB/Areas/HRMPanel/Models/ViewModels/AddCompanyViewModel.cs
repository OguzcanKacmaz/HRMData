using HRMData.Domain.Entities.Enums;
using HRMData.Domain.Enums;

namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class AddCompanyViewModel
    {
        public string? Name { get; set; }
        public string? MersisNo { get; set; }
        public string? TaxNumber { get; set; }
        public string? TaxAdministration { get; set; } 
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string? Logo { get; set; }
        public string? LogoMini { get; set; }
        public IFormFile? LogoPath { get; set; }
        public CompanyTitle CompanyTitle { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public int EmployeeCount { get; set; }
        public int FoundationYear { get; set; }
    }
}
