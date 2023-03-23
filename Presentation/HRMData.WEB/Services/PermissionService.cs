using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace HRMData.WEB.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IEmployeePermissionRequestRepository _employeePermission;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public PermissionService(IEmployeePermissionRequestRepository EmployeePermission, IEmployeeRepository EmployeeRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _employeePermission = EmployeePermission;
            _employeeRepository = EmployeeRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<bool> PermissionDateCheck(DateTime startDate, DateTime endDate)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            var permissions = await _employeePermission.GetApprovedAndInprogressRequestAsync(Employee.Id);
            foreach (var permission in permissions)
            {
                if (startDate >= permission.StartDate && startDate < permission.EndDate) return false;
                if (endDate >= permission.StartDate && endDate < permission.EndDate) return false;
                if (startDate <= permission.StartDate && endDate >= permission.EndDate) return false;
            }
            return true;
        }
    }
}
