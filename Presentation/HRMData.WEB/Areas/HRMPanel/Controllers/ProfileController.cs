using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;
using HRMData.WEB.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMData.WEB.Areas.HRMPanel.Controllers
{
    [Authorize]
    [Area("HRMPanel")]
    public class ProfileController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(IEmployeeService EmployeeService, IEmployeeRepository EmployeeRepository, UserManager<AppUser> userManager)
        {
            _employeeService = EmployeeService;
            _employeeRepository = EmployeeRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            var EmployeeViewModel = await _employeeService.GetMapEmployeeAsync(Employee.Id);
            var editEmployeeViewModel = await _employeeService.GetMapEditEmployeeAsync(Employee.Id);
            var tuple = (EmployeeViewModel, editEmployeeViewModel);
            return View(tuple);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee([Bind(Prefix = "Item2")] EditEmployeeViewModel editEmployee)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
               .Select(e => e.ErrorMessage).ToList();
                TempData["Error"] = errorMessages[0];//string.Join(" veya ", errorMessages);
                return View();
            }
            if (editEmployee.Address == null && editEmployee.PhotoPath == null && editEmployee.PhoneNumber == null)
            {
                TempData["Error"] = "Lütfen Güncellenecek Bilgileri Giriniz";
                return RedirectToAction(nameof(Index));
            }
            if (await _employeeService.EditEmployeeAsync(editEmployee))
                TempData["Success"] = "Bilgiler Güncellendi";
            return RedirectToAction(nameof(Index));
        }

    }
}
