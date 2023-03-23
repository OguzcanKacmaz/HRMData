using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMData.WEB.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeRepository _EmployeeRepository;

        public EmployeeController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IEmployeeRepository EmployeeRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _EmployeeRepository = EmployeeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task Logout()
        {
            HttpContext.Session.Remove("email");
            HttpContext.Session.Remove("photo");
            await _signInManager.SignOutAsync();
        }
        public async Task<IActionResult> Locked()
        {
            var user = await _userManager.GetUserAsync(User);
            var Employee = await _EmployeeRepository.GetUserEmployeeAsync(user.Id);
            HttpContext.Session.SetString("email", user.Email);
            HttpContext.Session.SetString("photo", Employee.Photo!);
            await _signInManager.SignOutAsync();

            return RedirectToAction("SignIn", "Home");
        }

    }
}
