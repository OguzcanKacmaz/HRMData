using AutoMapper;
using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.Domain.Enums;
using HRMData.WEB.Areas.HRMPanel.Models;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;
using HRMData.WEB.Interfaces;
using HRMData.WEB.Models;
using MernisService;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace HRMData.WEB.Services
{
    public class EmployeeService : IEmployeeService
    {
        //Giriş Yapan Kullanıcıyı Döndürmesi Gerekiyor. Şuan Sadece Bizim İstediğimizi Döndürecek
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeExpenseRepository _employeeExpenseRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IEmployeePermissionRequestRepository _employeePermission;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITitleRepository _titleRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmployeeAdvanceRepository _advanceRepository;

        public EmployeeService(IMapper mapper, IEmployeeAdvanceRepository advanceRepository, IEmployeeRepository EmployeeRepository, IEmployeeExpenseRepository EmployeeExpenseRepository, IWebHostEnvironment env, IEmployeePermissionRequestRepository EmployeePermission, UserManager<AppUser> userManager, ITitleRepository titleRepository, IDepartmentRepository departmentRepository, ICompanyRepository companyRepository, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _advanceRepository = advanceRepository;
            _employeeRepository = EmployeeRepository;
            _employeeExpenseRepository = EmployeeExpenseRepository;
            _env = env;
            _employeePermission = EmployeePermission;
            _userManager = userManager;
            _titleRepository = titleRepository;
            _departmentRepository = departmentRepository;
            _companyRepository = companyRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddAdvance(AddEmployeeAdvanceViewModel EmployeeAdvanceViewModel)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            DateTime utcDateTime = EmployeeAdvanceViewModel.RequestDate.ToUniversalTime();
            TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, turkeyZone);
            EmployeeAdvanceViewModel.RequestDate = turkeyDateTime;
            var EmployeeAdvance = _mapper.Map<EmployeeAdvance>(EmployeeAdvanceViewModel);
            EmployeeAdvance.EmployeeId = Employee.Id;
            if ((Employee!.RemainingAdvanceAmount >= EmployeeAdvanceViewModel.Amount) && EmployeeAdvanceViewModel.AdvancePaymentType == AdvancePaymentType.Individual)
            {
                Employee.RemainingAdvanceAmount -= EmployeeAdvanceViewModel.Amount;
                await _employeeRepository.UpdateAsync(Employee);
                await _advanceRepository.AddAsync(EmployeeAdvance);
                return true;
            }
            else if (EmployeeAdvanceViewModel.AdvancePaymentType == AdvancePaymentType.Institutional)
            {
                await _advanceRepository.AddAsync(EmployeeAdvance);
                return true;
            }
            return false;
        }

        public async Task AddExpense(AddEmployeeExpenseViewModel vm)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            if (vm.ExpenseDocumentPath != null)
            {
                vm.ExpenseDocument = ImageFileOperations.SaveFile(vm.ExpenseDocumentPath, _env);
            }
            DateTime utcDateTime = vm.RequestDate.ToUniversalTime();
            TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, turkeyZone);
            vm.RequestDate = turkeyDateTime;
            var EmployeeExpense = _mapper.Map<EmployeeExpense>(vm);
            EmployeeExpense.EmployeeId = Employee.Id;
            await _employeeExpenseRepository.AddAsync(EmployeeExpense);
        }

        public async Task<bool> AdvanceApprovalStatusCheck(string id)
        {
            var advance = await _advanceRepository.GetByIdAsync(id);
            if (advance == null) return false;
            if (advance.ApprovalStatus == ApprovalStatus.PendingApproval)
            {
                await _advanceRepository.DeleteAsync(advance);
                var Employee = await _employeeRepository.GetByIdAsync(advance.EmployeeId);
                Employee!.RemainingAdvanceAmount += advance.Amount;
                await _employeeRepository.UpdateAsync(Employee);
                return true;
            }

            return false;
        }

        public async Task<bool> EditEmployeeAsync(EditEmployeeViewModel vm)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            if (Employee == null)
                return false;
            if (Employee.Address == vm.Address && user.PhoneNumber == vm.PhoneNumber && vm.PhotoPath == null)
                return false;
            if (vm.Address != null)
                Employee.Address = vm.Address.Trim();
            if (vm.PhoneNumber != null)
                user.PhoneNumber = vm.PhoneNumber.Trim();
            Employee.Id = vm.Id;
            if (vm.PhotoPath != null)
            {
                if (Employee.Photo != null)
                {
                    ImageFileOperations.DeletePhoto(Employee.Photo, _env);
                    ImageFileOperations.DeletePhoto(Employee.PhotoMini!, _env);
                }
                Employee.Photo = ImageFileOperations.ResizeAddPhoto(vm.PhotoPath, 230, _env, "img");
                Employee.PhotoMini = ImageFileOperations.ResizeAddPhoto(vm.PhotoPath, 32, _env, "img");

            }
            await _userManager.UpdateAsync(user);
            await _employeeRepository.UpdateAsync(Employee);
            return true;
        }

        public async Task<bool> ExpenseApprovalStatusCheck(string id)
        {
            var expense = await _employeeExpenseRepository.GetByIdAsync(id);
            if (expense == null) return false;
            if (expense.ApprovalStatus == ApprovalStatus.PendingApproval)
            {
                ImageFileOperations.DeleteFile(expense.ExpenseDocument, _env);
                await _employeeExpenseRepository.DeleteAsync(expense);
                return true;
            }
            return false;
        }

        public async Task<EmployeeAdvanceTransactionsViewModel> GetAdvanceTransactionsViewModelAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            EmployeeAdvanceTransactionsViewModel advanceTransactionsViewModel = new()
            {
                AllAdvances = await _advanceRepository.GetAllOrderByAdvancesAsync(Employee.Id),
                ApprovedAdvances = await _advanceRepository.ApprovedAdvancesAsync(Employee.Id),
                InProgressAdvances = await _advanceRepository.InProgressAdvancesAsync(Employee.Id),
                RejectedAdvances = await _advanceRepository.RejectedAdvancesAsync(Employee.Id)
            };
            return advanceTransactionsViewModel;
        }
        public async Task<EmployeeAdvanceTransactionsViewModel> GetAllAdvanceTransactionsViewModelAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            EmployeeAdvanceTransactionsViewModel advanceTransactionsViewModel = new()
            {
                AllAdvances = await _advanceRepository.GetAllAdvancesAsync(Employee.Id),
                ApprovedAdvances = await _advanceRepository.AllApprovedAdvancesAsync(Employee.Id),
                InProgressAdvances = await _advanceRepository.AllInProgressAdvancesAsync(Employee.Id),
                RejectedAdvances = await _advanceRepository.AllRejectedAdvancesAsync(Employee.Id)
            };
            return advanceTransactionsViewModel;
        }
        public async Task<EmployeeExpenseTransactionsViewModel> GetAllExpenseTransactionsViewModelAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            EmployeeExpenseTransactionsViewModel employeeExpenseTransactionsViewModel = new()
            {
                AllExpenses = await _employeeExpenseRepository.GetAllExpenseAsync(Employee.Id),
                ApprovedExpenses = await _employeeExpenseRepository.AllApprovedExpenseAsync(Employee.Id),
                InProgressExpenses = await _employeeExpenseRepository.AllInProgressExpenseAsync(Employee.Id),
                RejectedExpenses = await _employeeExpenseRepository.AllRejectedExpenseAsync(Employee.Id)
            };
            return employeeExpenseTransactionsViewModel;
        }
        public async Task<EmployeePermissionRequestTransactionsViewModel> GetAllPermissonTransactionsViewModelAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            EmployeePermissionRequestTransactionsViewModel employeePermissionRequestTransactionsViewModel = new()
            {
                AllPermissionRequest = await _employeePermission.GetAllPermissionAsync(Employee.Id),
                ApprovedPermissionRequest = await _employeePermission.AllApprovedPermissionAsync(Employee.Id),
                InProgressPermissionRequest = await _employeePermission.AllInProgressPermissionAsync(Employee.Id),
                RejectedPermissionRequest = await _employeePermission.AllRejectedPermissionAsync(Employee.Id)
            };
            return employeePermissionRequestTransactionsViewModel;
        }
        public async Task<EmployeeCardViewModel> GetCardMapEmployee(string id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            var titleName = await _titleRepository.FindNameAsync(employee!.TitleId!);
            var cardEmploye = _mapper.Map<EmployeeCardViewModel>(employee);
            cardEmploye.TitleName = titleName;
            return cardEmploye;
        }

        public async Task<EmployeeExpenseTransactionsViewModel> GetExpenseTransactionsViewModelAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            EmployeeExpenseTransactionsViewModel EmployeeExpenseTransactionsViewModel = new()
            {
                AllExpenses = await _employeeExpenseRepository.GetAllOrderByExpenseAsync(Employee.Id),
                ApprovedExpenses = await _employeeExpenseRepository.ApprovedExpenseAsync(Employee.Id),
                InProgressExpenses = await _employeeExpenseRepository.InProgressExpenseAsync(Employee.Id),
                RejectedExpenses = await _employeeExpenseRepository.RejectedExpenseAsync(Employee.Id)
            };
            return EmployeeExpenseTransactionsViewModel;
        }

        public async Task<AddEmployeeAdvanceViewModel> GetMapAdvanceViewModelAsync(string id)
        {
            var Advance = await _advanceRepository.GetByIdAsync(id);
            return _mapper.Map<AddEmployeeAdvanceViewModel>(Advance);
        }

        public async Task<EditEmployeeViewModel> GetMapEditEmployeeAsync(string id)
        {
            var Employee = await _employeeRepository.GetByIdAsync(id);
            var editEmployeeViewModel = _mapper.Map<EditEmployeeViewModel>(Employee);
            editEmployeeViewModel.PhoneNumber = Employee!.AppUser.PhoneNumber;
            return editEmployeeViewModel;
        }

        public async Task<AddEmployeeExpenseViewModel> GetMapExpenseViewModelAsync(string id)
        {
            var Expence = await _employeeExpenseRepository.GetByIdAsync(id);
            return _mapper.Map<AddEmployeeExpenseViewModel>(Expence);
        }

        public async Task<EmployeeViewModel> GetMapEmployeeAsync(string id)
        {
            var Employee = await _employeeRepository.IncludeEmployee(id);
            var titleName = await _titleRepository.FindNameAsync(Employee!.TitleId!);
            var EmployeeViewModel = _mapper.Map<EmployeeViewModel>(Employee);
            EmployeeViewModel.PhoneNumber = Employee.AppUser.PhoneNumber;
            EmployeeViewModel.Email = Employee.AppUser.Email;
            EmployeeViewModel.TitleName = titleName;
            return EmployeeViewModel;
        }


        public async Task<EmployeePermissionRequestTransactionsViewModel> GetPermissionTransactionsViewModelAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            EmployeePermissionRequestTransactionsViewModel EmployeePermissionRequestTransactionsViewModel = new()
            {
                AllPermissionRequest = await _employeePermission.GetAllOrderByPermissionRequestAsync(Employee.Id),
                ApprovedPermissionRequest = await _employeePermission.ApprovedPermissionRequestAsync(Employee.Id),
                InProgressPermissionRequest = await _employeePermission.InProgressPermissionRequestAsync(Employee.Id),
                RejectedPermissionRequest = await _employeePermission.RejectedPermissionRequestAsync(Employee.Id)
            };
            return EmployeePermissionRequestTransactionsViewModel;
        }

        public async Task<bool> AddPermission(AddEmployeePermissionRequestViewModel permissionRequestViewModel)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            if (Employee == null) return false;
            DateTime utcDateTime = permissionRequestViewModel.RequestDate.ToUniversalTime();
            TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, turkeyZone);
            permissionRequestViewModel.RequestDate = turkeyDateTime;
            if (permissionRequestViewModel.PermissionType == PermissionType.AnnualLeave && Employee.RemainingAnnualLeaveDays < permissionRequestViewModel.NumOfDays)
                return false;
            if (permissionRequestViewModel.PermissionType == PermissionType.ExcusedAbsence && Employee.ExcusedAbsenceDay == 0)
                return false;
            if (permissionRequestViewModel.DocumentPath != null)
            {
                permissionRequestViewModel.Document = ImageFileOperations.SaveFile(permissionRequestViewModel.DocumentPath, _env);
            }
            if (permissionRequestViewModel.PermissionType == PermissionType.ExcusedAbsence && Employee.ExcusedAbsenceDay != 0 && Employee.ResetDate > DateTime.Now)
            {
                Employee.ExcusedAbsenceDay = 0;
                var EmployeePermission = _mapper.Map<EmployeePermissionRequest>(permissionRequestViewModel);
                EmployeePermission.EmployeeId = Employee.Id;
                await _employeeRepository.UpdateAsync(Employee);
                await _employeePermission.AddAsync(EmployeePermission);
                return true;
            }

            if (permissionRequestViewModel.PermissionType == PermissionType.AnnualLeave && Employee.RemainingAnnualLeaveDays >= permissionRequestViewModel.NumOfDays)
            {
                Employee.RemainingAnnualLeaveDays -= permissionRequestViewModel.NumOfDays;
                var EmployeePermission = _mapper.Map<EmployeePermissionRequest>(permissionRequestViewModel);
                EmployeePermission.EmployeeId = Employee.Id;
                await _employeeRepository.UpdateAsync(Employee);
                await _employeePermission.AddAsync(EmployeePermission);
                return true;
            }

            if (permissionRequestViewModel.PermissionType != PermissionType.AnnualLeave && permissionRequestViewModel.PermissionType != PermissionType.ExcusedAbsence)
            {
                var EmployeePermission = _mapper.Map<EmployeePermissionRequest>(permissionRequestViewModel);
                EmployeePermission.EmployeeId = Employee.Id;
                await _employeeRepository.UpdateAsync(Employee);
                await _employeePermission.AddAsync(EmployeePermission);
                return true;
            }
            return true;

        }

        public async Task<Gender> GetEmployeeGenderAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            return Employee.Gender;
        }

        public async Task<bool> PermissionApprovalStatusCheck(string id)
        {
            var permission = await _employeePermission.GetByIdAsync(id);
            if (permission == null) return false;
            var Employee = await _employeeRepository.GetByIdAsync(permission.EmployeeId);
            if (Employee == null) return false;

            if (permission.ApprovalStatus == ApprovalStatus.PendingApproval && permission.PermissionType == PermissionType.ExcusedAbsence && Employee.ResetDate > DateTime.Now)
            {
                Employee.ExcusedAbsenceDay = 1;
                ImageFileOperations.DeleteFile(permission.Document!, _env);
                await _employeePermission.DeleteAsync(permission);
                await _employeeRepository.UpdateAsync(Employee);
                return true;
            }
            else if (permission.ApprovalStatus == ApprovalStatus.PendingApproval && permission.PermissionType == PermissionType.AnnualLeave && Employee.ResetDate > DateTime.Now)
            {
                Employee.RemainingAnnualLeaveDays += permission.NumOfDays;
                ImageFileOperations.DeleteFile(permission.Document!, _env);
                await _employeePermission.DeleteAsync(permission);
                await _employeeRepository.UpdateAsync(Employee);
                return true;
            }
            else
            {
                ImageFileOperations.DeleteFile(permission.Document!, _env);
                await _employeePermission.DeleteAsync(permission);
                await _employeeRepository.UpdateAsync(Employee);
                return true;
            }

        }

        public async Task<PermissionRequestNumOfDaysViewModel> GetMapPermissionAsync(PermissionRequestNumOfDays permissionRequest)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            var mapping = _mapper.Map<PermissionRequestNumOfDaysViewModel>(permissionRequest);
            mapping.AnnualLeaveDay = Employee!.RemainingAnnualLeaveDays;
            mapping.ExcusedAbsenceDay = Employee.ExcusedAbsenceDay;
            return mapping;
        }

        public async Task<double> RemainingAnnualLeave()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var Employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            var RemainingAnnualLeaveCount = Employee.RemainingAnnualLeaveDays;
            return RemainingAnnualLeaveCount;
        }

        public async Task<EmployeeMailViewModel> AddPersonel(AddEmployeeViewModel vm)
        {
            var email = EmailGenerator.GenerateEmail(vm.FirstName!, vm.LastName!, vm.MiddleName!);
            var company = await _companyRepository.GetByIdAsync(vm.CompanyId!);
            AppUser personel = new()
            {
                Email = email,
                PhoneNumber = vm.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = true,
                UserName = email,
                Employee = new()
                {
                    Address = vm.Address!,
                    AnnualLeaveDays = ((DateTime.Now - vm.HireDate).TotalDays >= 365 && (DateTime.Now - vm.HireDate).TotalDays <= 1825) ? 14 : (DateTime.Now - vm.HireDate).TotalDays > 1825 ? 24 : 0,
                    RemainingAnnualLeaveDays = ((DateTime.Now - vm.HireDate).TotalDays >= 365 && (DateTime.Now - vm.HireDate).TotalDays <= 1825) ? 14 : (DateTime.Now - vm.HireDate).TotalDays > 1825 ? 24 : 0,
                    DepartmentId = vm.DepartmentId,
                    CompanyId = vm.CompanyId,
                    DateOfBirth = vm.DateOfBirth,
                    FirstName = WordCapitalization(vm.FirstName!.Trim()),
                    Gender = vm.Gender,
                    HireDate = vm.HireDate,
                    LastName = WordCapitalization(vm.LastName!.Trim()),
                    MaxPaymentAmount = vm.Salary * 3,
                    Salary = vm.Salary,
                    TitleId = vm.TitleId!,
                    RemainingAdvanceAmount = vm.Salary * 3,
                    ResetDate = vm.HireDate.AddYears(1),//.AddYears((DateTime.Now.Year - vm.HireDate.Year) + 1)
                    TcIdentificationNumber = vm.TcIdentificationNumber!,
                    TerminationDate = vm.TerminationDate,
                    PlaceOfBirth = vm.OtherPlaceOfBirth ?? vm.PlaceOfBirth.ToString()!,
                }
            };
            if (vm.TerminationDate == null)
            {
                if (vm.HireDate > DateTime.Now)
                    personel.Employee.IsActive = Domain.Entities.Enums.Status.Passive;
                if (vm.HireDate <= DateTime.Now)
                    personel.Employee.IsActive = Domain.Entities.Enums.Status.Active;
            }
            if (vm.TerminationDate != null)
            {
                if (vm.TerminationDate <= DateTime.Now)
                    personel.Employee.IsActive = Domain.Entities.Enums.Status.Passive;
                if (vm.TerminationDate > DateTime.Now && vm.HireDate <= DateTime.Now)
                    personel.Employee.IsActive = Domain.Entities.Enums.Status.Active;
                else
                    personel.Employee.IsActive = Domain.Entities.Enums.Status.Passive;
            }
            if (vm.SecondLastName != null)
            {
                personel.Employee.SecondLastName = WordCapitalization(vm.SecondLastName!.Trim());
            }
            if (vm.MiddleName != null)
            {
                personel.Employee.MiddleName = WordCapitalization(vm.MiddleName!.Trim());
            }
            if (vm.PhotoPath != null)
            {
                personel.Employee.Photo = ImageFileOperations.ResizeAddPhoto(vm.PhotoPath, 230, _env, "img");
                personel.Employee.PhotoMini = ImageFileOperations.ResizeAddPhoto(vm.PhotoPath, 32, _env, "img");
            }
            if (vm.PhotoPath == null)
            {
                personel.Employee.Photo = vm.Gender == Gender.Female ? "defaultfemale.jpg" : "default.jpg";
                personel.Employee.PhotoMini = vm.Gender == Gender.Female ? "defaultfemalemini.jpg" : "defaultmini.jpg";
            }
            var password = PasswordGenerator.GeneratePassword();
            await _userManager.CreateAsync(personel, password);
            company!.RemainingEmployeeCount--;
            await _companyRepository.UpdateAsync(company);
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var IsInAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (IsInAdmin)
                await _userManager.AddToRoleAsync(personel, "Manager");
            else
                await _userManager.AddToRoleAsync(personel, "Personel");
            var EmployeeMailViewModel = new EmployeeMailViewModel()
            {
                Employee = personel,
                Password = password,
            };
            return EmployeeMailViewModel;
        }
        public async Task<List<EmployeeViewModel>> GetAllEmployeeListAsync(string RoleName)
        {
            List<EmployeeViewModel> allEmployees = new();
            var employees = await _employeeRepository.GetAllRoleEmployee(RoleName);
            if (employees.Count != 0)
            {
                foreach (var e in employees)
                {
                    var employee = _mapper.Map<EmployeeViewModel>(e);
                    employee.Email = e.AppUser.Email;
                    employee.PhoneNumber = e.AppUser.PhoneNumber;
                    employee.DepartmentName = e.Department!.Name;
                    employee.CompanyName = e.Company!.Name;
                    employee.TitleName = await _titleRepository.FindNameAsync(e.TitleId!);
                    allEmployees.Add(employee);
                }
            }
            return allEmployees;
        }

        public async Task<bool> TCAuthenticationAsync(string Tc, string FirstName, string middleName, string? LastName, DateTime BirthOfDate)
        {
            var fullName = middleName == null ? WordCapitalization(FirstName.Trim()) : WordCapitalization(FirstName.Trim()) + " " + WordCapitalization(middleName.Trim());
            var year = BirthOfDate.Year;
            var client = new MernisService.KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
            var response = await client.TCKimlikNoDogrulaAsync(Convert.ToInt64(Tc), fullName, LastName, year);
            var result = response.Body.TCKimlikNoDogrulaResult;
            return result;
        }

        public async Task<AddEmployeeViewModel> GetAddEmployeeViewModelAsync(AddEmployeeViewModel? vm = null)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext!.User);
            var employee = await _employeeRepository.GetUserEmployeeAsync(user.Id);
            var IsInAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (vm == null)
            {
                var employeeViewModel = new AddEmployeeViewModel()
                {
                    Departments = await _departmentRepository.GetAllAsync(),
                    Titles = await _titleRepository.GetAllAsync(),
                    Company = IsInAdmin ? await _companyRepository.GetAllAsync() : await _companyRepository.GetCompanyAsync(employee.CompanyId!)
                };
                return employeeViewModel;
            }
            vm.Departments = await _departmentRepository.GetAllAsync();
            vm.Titles = await _titleRepository.GetAllAsync();
            vm.Company = IsInAdmin ? await _companyRepository.GetAllAsync() : await _companyRepository.GetCompanyAsync(employee.CompanyId!);
            return vm;
        }
        private static string WordCapitalization(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return string.Empty;
            }

            char[] letters = word.ToLower().ToCharArray();
            letters[0] = char.ToUpper(letters[0]);

            return new string(letters);
        }

        public async Task DeniedAdvance(string id)
        {
            var advance = await _advanceRepository.GetByIdAsync(id);
            advance!.ApprovalStatus = ApprovalStatus.Denied;
            DateTime utcDateTime = DateTime.Now.ToUniversalTime();
            TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, turkeyZone);
            advance.ReplyDate = turkeyDateTime;
            await _advanceRepository.UpdateAsync(advance);
        }

        public async Task ApproveAdvance(string id)
        {
            var advance = await _advanceRepository.GetByIdAsync(id);
            advance!.ApprovalStatus = ApprovalStatus.Approved;
            DateTime utcDateTime = DateTime.Now.ToUniversalTime();
            TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, turkeyZone);
            advance.ReplyDate = turkeyDateTime;
            await _advanceRepository.UpdateAsync(advance);
        }
        public async Task DeniedExpense(string id)
        {
            var expense = await _employeeExpenseRepository.GetByIdAsync(id);
            expense!.ApprovalStatus = ApprovalStatus.Denied;
            DateTime utcDateTime = DateTime.Now.ToUniversalTime();
            TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, turkeyZone);
            expense.ReplyDate = turkeyDateTime;
            await _employeeExpenseRepository.UpdateAsync(expense);
        }

        public async Task ApproveExpense(string id)
        {
            var expense = await _employeeExpenseRepository.GetByIdAsync(id);
            expense!.ApprovalStatus = ApprovalStatus.Approved;
            DateTime utcDateTime = DateTime.Now.ToUniversalTime();
            TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, turkeyZone);
            expense.ReplyDate = turkeyDateTime;
            await _employeeExpenseRepository.UpdateAsync(expense);
        }
        public async Task DeniedPermission(string id)
        {
            var permission = await _employeePermission.GetByIdAsync(id);
            permission!.ApprovalStatus = ApprovalStatus.Denied;
            DateTime utcDateTime = DateTime.Now.ToUniversalTime();
            TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, turkeyZone);
            permission.ReplyDate = turkeyDateTime;
            await _employeePermission.UpdateAsync(permission);
        }

        public async Task ApprovePermission(string id)
        {
            var permission = await _employeePermission.GetByIdAsync(id);
            permission!.ApprovalStatus = ApprovalStatus.Approved;
            DateTime utcDateTime = DateTime.Now.ToUniversalTime();
            TimeZoneInfo turkeyZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
            DateTime turkeyDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, turkeyZone);
            permission.ReplyDate = turkeyDateTime;
            await _employeePermission.UpdateAsync(permission);
        }
    }
}
