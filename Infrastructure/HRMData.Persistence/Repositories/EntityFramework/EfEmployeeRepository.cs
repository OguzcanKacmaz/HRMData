using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HRMData.Persistence.Repositories.EntityFramework
{
    public class EfEmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public EfEmployeeRepository(ApplicationDbContext db, UserManager<AppUser> userManager) : base(db)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<List<Employee>> AllIncludeEmployees()
        {
            var employees = await _db.Employees.Include(x => x.AppUser).ToListAsync();
            return employees;
        }

        public async Task<List<Employee>> GetAllPersonelsAsync()
        {
            List<Employee> personels = new();
            var employees = await _db.Employees.Include(x => x.AppUser).Include(x => x.Company).Include(x => x.Department).ToListAsync();
            foreach (var employee in employees)
            {
                var IsInPersonel = await _userManager.IsInRoleAsync(employee.AppUser, "Personel");
                if (IsInPersonel)
                    personels.Add(employee);
            }
            return personels;
        }

        public async Task<List<Employee>> GetAllRoleEmployee(string RoleName)
        {
            List<Employee> employees = new();
            var users = await _userManager.GetUsersInRoleAsync(RoleName);
            foreach (var user in users)
            {
                var employee = await _db.Employees.Include(x => x.Department).Include(x => x.Company).FirstOrDefaultAsync(x => x.AppUserId == user.Id);
                employees.Add(employee!);
            }
            return employees;
        }

        public async Task<Employee> GetUserEmployeeAsync(string id)
        {
            var Employee = await _db.Employees.FirstOrDefaultAsync(x => x.AppUserId == id);
            return Employee!;
        }

        public async Task<Employee> IncludeEmployee(string id)
        {
            var IncludEmployee = await _db.Employees.Include(x => x.AppUser).Include(x => x.Department).ThenInclude(x => x!.Company).FirstOrDefaultAsync(x => x.Id == id);

            return IncludEmployee!;
        }

        public async Task<bool> TcControlAsync(string Tc, string DepartmentId)
        {
            var tc = await _db.Employees.Where(x => x.DepartmentId == DepartmentId).FirstOrDefaultAsync(x => x.TcIdentificationNumber == Tc);
            if (tc == null)
                return true;
            return false;
        }
    }
}
