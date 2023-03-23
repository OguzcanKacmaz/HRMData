using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMData.Persistence.Repositories.EntityFramework
{
    public class EfCompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public EfCompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<List<Company>> GetAllIncludeEmployees()
        {
            var companies=await _db.Companies.Include(x => x.Employees).ToListAsync();
            return companies;
        }

        public async Task<List<Company>> GetCompanyAsync(string id)
        {
            var company = await _db.Companies.Where(x => x.Id == id).ToListAsync();
            return company!;
        }

        public async Task<Company> GetIncludeEmployees(string CompanyId)
        {
            var company = await _db.Companies.Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == CompanyId);
            return company!;
        }
    }
}
