using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Entensions;
using HRMData.WEB.Interfaces;
using HRMData.WEB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMData.WEB.Areas.HRMPanel.Controllers
{
    [Authorize(Roles = "Manager")]
    [Area("HRMPanel")]
    public class ManagerController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailServices _emailServices;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyService _companyService;

        public ManagerController(IEmployeeService employeeService, UserManager<AppUser> userManager, IEmailServices emailServices, IEmployeeRepository employeeRepository,ICompanyService companyService)
        {
            _employeeService = employeeService;
            _userManager = userManager;
            _emailServices = emailServices;
            _employeeRepository = employeeRepository;
            _companyService = companyService;
        }
        public async Task<IActionResult> Index()
        {
            var personels = await _employeeService.GetAllEmployeeListAsync("Personel");
            return View(personels);
        }
        public async Task<IActionResult> AddPersonel()
        {
            var addEmployeeViewModel = await _employeeService.GetAddEmployeeViewModelAsync();
            return View(addEmployeeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddPersonel(AddEmployeeViewModel vm)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelErrorList(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
                var viewModel = await _employeeService.GetAddEmployeeViewModelAsync(vm);
                return View(viewModel);
            }
            if (!await _employeeService.TCAuthenticationAsync(vm.TcIdentificationNumber!, vm.FirstName!, vm.MiddleName!, vm.LastName, vm.DateOfBirth))
            {
                ModelState.AddModelError(string.Empty, "Personel Kimlik Bilgileri Doğru Değildir");
                var viewModel = await _employeeService.GetAddEmployeeViewModelAsync(vm);
                return View(viewModel);
            }
            if (!await _employeeRepository.TcControlAsync(vm.TcIdentificationNumber!, vm.DepartmentId!))
            {
                ModelState.AddModelError(string.Empty, "Kayıtlı Böyle Bir Personel Vardır");
                var viewModel = await _employeeService.GetAddEmployeeViewModelAsync(vm);
                return View(viewModel);
            }
            if (!await _companyService.CompanyEmployeeCountCheck(vm.CompanyId!))
            {
                ModelState.AddModelError(string.Empty, "Şirkete Daha Fazla Personel Ekleyemezsiniz");
                var viewModel = await _employeeService.GetAddEmployeeViewModelAsync(vm);
                return View(viewModel);
            }
            var EmployeeMailViewModel = await _employeeService.AddPersonel(vm);
            string passwordChangeToken = await _userManager.GeneratePasswordResetTokenAsync(EmployeeMailViewModel.Employee!);
            var passwordChangeLink = Url.Action("ChangePassword", "Home", new { area = "", userId = EmployeeMailViewModel.Employee!.Id, Token = passwordChangeToken }, HttpContext.Request.Scheme);
            await _emailServices.SendPasswordChangeEmailAsync(passwordChangeLink!, EmployeeMailViewModel.Employee.Email, EmployeeMailViewModel.Password!);
            TempData["SuccessMessage"] = "Personel Başarılı Bir Şekilde Eklenmiştir";
            return RedirectToAction(nameof(AddPersonel));
        }
        
        public async Task<IActionResult> AdvanceRequest()
        {
            var advanceTransactions = await _employeeService.GetAllAdvanceTransactionsViewModelAsync();
            return View(advanceTransactions);
        }        
        public async Task<IActionResult> DeniedAdvaceRequest(string Id)
        {
            await _employeeService.DeniedAdvance(Id);
            TempData["Success"] = "Talep Reddetme İşlemi Başarılı";
            return RedirectToAction(nameof(AdvanceRequest));
        }
        public async Task<IActionResult> ApproveAdvanceRequest(string Id)
        {
            await _employeeService.ApproveAdvance(Id);
            TempData["Success"] = "Talep Onaylama İşlemi Başarılı";
            return RedirectToAction(nameof(AdvanceRequest));
        }
        public async Task<IActionResult> ExpenseRequest()
        {
            var expenseTransactions = await _employeeService.GetAllExpenseTransactionsViewModelAsync();
            return View(expenseTransactions);
        }
        public async Task<IActionResult> DeniedExpenseRequest(string Id)
        {
            await _employeeService.DeniedExpense(Id);
            TempData["Success"] = "Talep Reddetme İşlemi Başarılı";
            return RedirectToAction(nameof(ExpenseRequest));
        }
        public async Task<IActionResult> ApproveExpenseRequest(string Id)
        {
            await _employeeService.ApproveExpense(Id);
            TempData["Success"] = "Talep Onaylama İşlemi Başarılı";
            return RedirectToAction(nameof(ExpenseRequest));
        }
        public async Task<IActionResult> PermissionRequest()
        {
            var permissionTransactions = await _employeeService.GetAllPermissonTransactionsViewModelAsync();
            return View(permissionTransactions);
        }
        public async Task<IActionResult> DeniedPermissionRequest(string Id)
        {
            await _employeeService.DeniedPermission(Id);
            TempData["Success"] = "Talep Reddetme İşlemi Başarılı";
            return RedirectToAction(nameof(PermissionRequest));
        }
        public async Task<IActionResult> ApprovePermissionRequest(string Id)
        {
            await _employeeService.ApprovePermission(Id);
            TempData["Success"] = "Talep Onaylama İşlemi Başarılı";
            return RedirectToAction(nameof(PermissionRequest));
        }
    }
}
