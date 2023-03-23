using HRMData.Domain.Entities;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;

namespace HRMData.WEB.Interfaces
{
    public interface ICompanyService
    {
        public Task<List<CompanyViewModel>> GetAllCompanies();
        public Task<bool> CompanyEmployeeCountCheck(string companyId);
        public Task AddCompanyAsync(AddCompanyViewModel vm);
    }
}
