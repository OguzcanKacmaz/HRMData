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
    public class PermissionController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPermissionRequestNumOfDays _ofDays;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPermissionService _permissionService;

        public PermissionController(IEmployeeService EmployeeService, UserManager<AppUser> userManager, IPermissionRequestNumOfDays ofDays, IEmployeeRepository EmployeeRepository, IPermissionService permissionService)
        {
            _employeeService = EmployeeService;
            _userManager = userManager;
            _ofDays = ofDays;
            _employeeRepository = EmployeeRepository;
            _permissionService = permissionService;
        }
        public async Task<IActionResult> Index()
        {
            var permission = await _employeeService.GetPermissionTransactionsViewModelAsync();
            return View(permission);
        }

        public async Task<IActionResult> AddEmployeePermisson()
        {
            var EmployeeGender = await _employeeService.GetEmployeeGenderAsync();
            var days = await _ofDays.PermissionRequestNumOfDaysAsync();
            var permission = await _employeeService.GetMapPermissionAsync(days);
            var tuple = (permission, new AddEmployeePermissionRequestViewModel());
            TempData["Gender"] = EmployeeGender;//EmployeeGender; 
            TempData["RemainingAnnualLeave"] = await _employeeService.RemainingAnnualLeave();
            return View(tuple);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployeePermisson([Bind(Prefix = "Item2")] AddEmployeePermissionRequestViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
                TempData["Error"] = string.Join(" , ", errorMessages);//"Lütfen Bütün Alanları Doldurun";
                return RedirectToAction(nameof(AddEmployeePermisson));
            }
            if (!await _permissionService.PermissionDateCheck(vm.StartDate, vm.EndDate))
            {
                TempData["Error"] = "Talep Ettiğiniz Tarihlerde Başka Bir Talebiniz Bulunmaktadır";
                return RedirectToAction(nameof(AddEmployeePermisson));
            }
            if (!await _employeeService.AddPermission(vm))
            {
                TempData["Error"] = "Talep Ettiğiniz İzin Gün Sayısı Kalan İzin Gün Sayınızdan Fazladır";
                return RedirectToAction(nameof(AddEmployeePermisson));
            }

            TempData["Success"] = "İzin Talebi Oluşturma İşlemi Başarılı";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CancelPermission(string permissionid)
        {
            if (await _employeeService.PermissionApprovalStatusCheck(permissionid))
            {
                TempData["Success"] = "İptal İşlemi Başarılı";
            }
            return RedirectToAction("Index");
        }

    }
}
