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
    public class AdvanceController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<AppUser> _userManager;

        public AdvanceController(IEmployeeService EmployeeService, UserManager<AppUser> userManager)
        {
            _employeeService = EmployeeService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var advanceTransactions = await _employeeService.GetAdvanceTransactionsViewModelAsync();
            return View(advanceTransactions);
        }
        public async Task<IActionResult> CancelAdvance(string id)
        {
            if (await _employeeService.AdvanceApprovalStatusCheck(id))
            {
                TempData["Success"] = "İptal İşlemi Başarılı";
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddEmployeeAdvance()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployeeAdvance(AddEmployeeAdvanceViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _employeeService.AddAdvance(vm))
            {
                TempData["Success"] = "İşleminiz Başarılı";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Talep Ettiğiniz Tutar, Yıllık Kalan Tutarınızdan Fazla";
            return View();

        }
    }
}
