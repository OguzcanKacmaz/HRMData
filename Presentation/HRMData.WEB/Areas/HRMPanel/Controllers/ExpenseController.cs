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
    public class ExpenseController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<AppUser> _userManager;

        public ExpenseController(IEmployeeService EmployeeService, UserManager<AppUser> userManager)
        {
            _employeeService = EmployeeService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var expense = await _employeeService.GetExpenseTransactionsViewModelAsync();
            return View(expense);
        }

        public IActionResult AddEmployeeExpense()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeExpense(AddEmployeeExpenseViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
                TempData["Error"] = string.Join(" , ", errorMessages);//"Lütfen Bütün Alanları Doldurun";
                return View();
            }

            await _employeeService.AddExpense(vm);
            TempData["Success"] = "Harcama Talebi Oluşturma İşlemi Başarılı";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelExpense(string id)
        {
            if (await _employeeService.ExpenseApprovalStatusCheck(id))
            {
                TempData["Success"] = "İptal İşlemi Başarılı";
            }
            return RedirectToAction("Index");
        }
    }
}
