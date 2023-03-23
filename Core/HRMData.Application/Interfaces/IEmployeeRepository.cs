using HRMData.Domain.Entities;

namespace HRMData.Application.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> IncludeEmployee(string EmployeeId);
        Task<List<Employee>> AllIncludeEmployees();
        Task<Employee> GetUserEmployeeAsync(string UserId);
        Task<List<Employee>> GetAllPersonelsAsync();
        Task<bool> TcControlAsync(string Tc, string DepartmentId);
        Task<List<Employee>> GetAllRoleEmployee(string RoleName);

    }
}
