using HRMData.Domain.Entities;

namespace HRMData.Application.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<List<Company>> GetCompanyAsync(string id);
        Task<Company> GetIncludeEmployees(string CompanyId);
        Task<List<Company>> GetAllIncludeEmployees();
    }
}
