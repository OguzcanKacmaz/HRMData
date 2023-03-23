using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.Infrastructure.Services;
using HRMData.Persistence.Repositories.EntityFramework;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;
using HRMData.WEB.Entensions;
using HRMData.WEB.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMData.WEB.Areas.HRMPanel.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("HRMPanel")]
    public class AdminController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmailServices _emailServices;
        private readonly ICompanyService _companyService;

        public AdminController(IEmployeeService employeeService,UserManager<AppUser> userManager,IEmployeeRepository employeeRepository,IEmailServices emailServices,ICompanyService companyService)
        {
            _employeeService = employeeService;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
            _emailServices = emailServices;
            _companyService = companyService;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeeListAsync("Manager");
            return View(employees);
        }
        public async Task<IActionResult> AddManager()
        {
            var addEmployeeViewModel = await _employeeService.GetAddEmployeeViewModelAsync();
            return View(addEmployeeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddManager(AddEmployeeViewModel vm)
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
                ModelState.AddModelError(string.Empty, "Şirket Yöneticisi Kimlik Bilgileri Doğru Değildir");
                var viewModel = await _employeeService.GetAddEmployeeViewModelAsync(vm);
                return View(viewModel);
            }
            if (!await _employeeRepository.TcControlAsync(vm.TcIdentificationNumber!,vm.DepartmentId!))
            {
                ModelState.AddModelError(string.Empty, "Kayıtlı Böyle Bir Şirket Yöneticisi Vardır");
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
            TempData["SuccessMessage"] = "Şirket Yöneticisi Başarılı Bir Şekilde Eklenmiştir";
            return RedirectToAction(nameof(AddManager));
        }
        public async Task<IActionResult> ListCompany()
        {
            var companies= await _companyService.GetAllCompanies();
            return View(companies);
        }
        public IActionResult AddCompany()
        {            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCompany(AddCompanyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelErrorList(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList());
                return View(vm);
            }
            await _companyService.AddCompanyAsync(vm);
            TempData["SuccessMessage"] = "Şirket Başarılı Bir Şekilde Eklenmiştir";
            return RedirectToAction(nameof(ListCompany));
        }
    }
}
