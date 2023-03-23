using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMData.WEB.Areas.HRMPanel.ViewComponents
{
    public class LeftUserViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository _EmployeeRepo;
        private readonly IEmployeeService _cardEmployee;
        private readonly UserManager<AppUser> _userManager;

        public LeftUserViewComponent(IEmployeeRepository EmployeeRepo, IEmployeeService cardEmployee, UserManager<AppUser> userManager)
        {
            _EmployeeRepo = EmployeeRepo;
            _cardEmployee = cardEmployee;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var Employee = await _EmployeeRepo.GetUserEmployeeAsync(user.Id);
            var cardViewModel = await _cardEmployee.GetCardMapEmployee(Employee.Id);
            return View(cardViewModel);
        }
    }
}
