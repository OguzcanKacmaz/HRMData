using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Entensions;
using HRMData.WEB.Models;
using HRMData.WEB.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HRMData.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IEmailServices _emailServices;

        public HomeController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IEmployeeRepository EmployeeRepository, IEmailServices emailServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _EmployeeRepository = EmployeeRepository;
            _emailServices = emailServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel signInViewModel, string? returnUrl = null!)
        {
            if (!ModelState.IsValid)
                return View();
            returnUrl ??= Url.Action("Index", "Profile", new { area = "HRMPanel" })!;

            var hasUser = await _userManager.FindByEmailAsync(signInViewModel.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email/Şifre Yanlış");
                return View();
            }
            var Employee = await _EmployeeRepository.GetUserEmployeeAsync(hasUser.Id);
            var result = await _userManager.CheckPasswordAsync(hasUser, signInViewModel.Password);
            if (Employee.IsActive == Domain.Entities.Enums.Status.Passive && result)
            {
                ModelState.AddModelError(string.Empty, "Üyeliğiniz Henüz Aktif Olmamıştır.. Eğer Bir Yanlışlık Olduğunu Düşünüyorsanız Yöneticinize Başvurunuz");
                return View();
            }
            if (Employee.IsActive == Domain.Entities.Enums.Status.Passive)
            {
                ModelState.AddModelError(string.Empty, "Email/Şifre Yanlış");
                return View();
            }
            if (Employee.RandomPassword)
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                TempData["CurrenUser"] = JsonSerializer.Serialize(Employee, options);
                return RedirectToAction(nameof(ChangePassword));
            }
            var signInresult = await _signInManager.PasswordSignInAsync(hasUser, signInViewModel.Password, isPersistent: signInViewModel.RememberMe, lockoutOnFailure: true);
            if (signInresult.Succeeded)
            {
                return Redirect(returnUrl);
            }

            if (signInresult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Çok Fazla Deneme Yaptınız.3 Dk Boyunca Giremezsiniz");
                return View();
            }

            ModelState.AddModelError(string.Empty, "Email/Şifre Yanlış");
            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel passwordResetViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Mail Adresi Boş Olamaz");
                return View();
            }
            var hasUser = await _userManager.FindByEmailAsync(passwordResetViewModel.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Bu Email Adresine Sahip Kullanıcı Bulunamamıştır");
                return View();
            }
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);
            var passwordResetLink = Url.Action("ResetPassword", "Home", new { userId = hasUser.Id, Token = passwordResetToken }, HttpContext.Request.Scheme);
            //Email Service
            await _emailServices.SendResetPasswordEmailAsync(passwordResetLink!, passwordResetViewModel.Email!);
            TempData["SuccessMessage"] = "Şifre Sıfırlama Bağlantısı Mail Adresinize Gönderilmiştir.";
            return RedirectToAction(nameof(ForgetPassword));
        }

        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var userId = TempData["userId"];
            var token = TempData["token"];
            if (userId == null && token == null)
                ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamamıştır Yada Bağlantı Süresi Doldu");
            if (!ModelState.IsValid)
                return View();
            var hasUser = await _userManager.FindByIdAsync(userId!.ToString());
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamamıştır");
                return View();
            }

            var result = await _userManager.ResetPasswordAsync(hasUser, token!.ToString(), resetPasswordViewModel.Password);
            if (result.Succeeded)
                TempData["SuccessMessage"] = "Şifreniz Başırıyla Yenilenmiştir";

            ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());

            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ChangePassword(string userId, string token)
        {
            if (TempData["CurrenUser"] != null)
                return RedirectToAction("SignIn", "Home");

            if (userId == null)
            {
                var User = TempData["CurrenUser"]!.ToString();
                var Currentuser = JsonSerializer.Deserialize<Employee>(User!);
                TempData["userId"] = Currentuser!.AppUserId;
                TempData["token"] = token;
                return View();
            }
            TempData["userId"] = userId;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            var userId = TempData["userId"]!.ToString();
            var token = TempData["token"];
            var employee = await _EmployeeRepository.GetUserEmployeeAsync(userId!);
            if (employee == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamamıştır Yada Bağlantı Süresi Doldu");
                return View();
            }
            var user = await _EmployeeRepository.IncludeEmployee(employee.Id);
            var checkresult = await _userManager.CheckPasswordAsync(user.AppUser, vm.OldPassword);
            if (userId == null && token == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamamıştır Yada Bağlantı Süresi Doldu");
                return View();
            }
            if (!checkresult)
            {
                ModelState.AddModelError(string.Empty, "Geçici Şifreniz Yanlış");
                TempData["userId"] = userId;
                TempData["token"] = token;
                return View();
            }
            var changeResult = await _userManager.ChangePasswordAsync(user.AppUser, vm.OldPassword, vm.NewPassword);
            if (!changeResult.Succeeded)
            {
                ModelState.AddModelErrorList(changeResult.Errors.Select(x => x.Description).ToList());
                TempData["userId"] = userId;
                TempData["token"] = token;
                return View();
            }
            await _userManager.UpdateSecurityStampAsync(user.AppUser);
            await _signInManager.SignOutAsync();
            user.RandomPassword = false;
            await _EmployeeRepository.UpdateAsync(user);
            TempData["SuccessMessage"] = "Şifreniz Başarılı Bir Şekilde Değiştirilmiştir";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
