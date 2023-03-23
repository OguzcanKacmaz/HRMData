using HRMData.Domain.Entities;
using HRMData.Domain.Enums;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;

namespace HRMData.WEB.Interfaces
{
    public interface IEmployeeService
    {
        Task<EmployeeViewModel> GetMapEmployeeAsync(string id);
        Task<EmployeeCardViewModel> GetCardMapEmployee(string id);
        Task<EmployeeExpenseTransactionsViewModel> GetExpenseTransactionsViewModelAsync();
        Task<AddEmployeeExpenseViewModel> GetMapExpenseViewModelAsync(string id);
        Task AddExpense(AddEmployeeExpenseViewModel vm);
        Task<bool> ExpenseApprovalStatusCheck(string id);
        Task<bool> PermissionApprovalStatusCheck(string id);
        Task<EmployeeAdvanceTransactionsViewModel> GetAdvanceTransactionsViewModelAsync();
        Task<EmployeeAdvanceTransactionsViewModel> GetAllAdvanceTransactionsViewModelAsync();
        Task<EmployeeExpenseTransactionsViewModel> GetAllExpenseTransactionsViewModelAsync();
        Task<EmployeePermissionRequestTransactionsViewModel> GetAllPermissonTransactionsViewModelAsync();
        Task<EmployeePermissionRequestTransactionsViewModel> GetPermissionTransactionsViewModelAsync();
        Task<AddEmployeeAdvanceViewModel> GetMapAdvanceViewModelAsync(string id);
        Task<bool> AdvanceApprovalStatusCheck(string id);
        Task<bool> AddAdvance(AddEmployeeAdvanceViewModel EmployeeAdvanceViewModel);
        Task<bool> AddPermission(AddEmployeePermissionRequestViewModel permissionRequestViewModel);
        Task<bool> EditEmployeeAsync(EditEmployeeViewModel vm);
        Task<EditEmployeeViewModel> GetMapEditEmployeeAsync(string id);
        Task<PermissionRequestNumOfDaysViewModel> GetMapPermissionAsync(PermissionRequestNumOfDays vm);
        Task<Gender> GetEmployeeGenderAsync();
        Task<double> RemainingAnnualLeave();
        Task<List<EmployeeViewModel>> GetAllEmployeeListAsync(string RoleName);
        Task<EmployeeMailViewModel> AddPersonel(AddEmployeeViewModel vm);
        Task<bool> TCAuthenticationAsync(string Tc, string FirstName, string middleName, string? LastName, DateTime BirthOfDate);
        Task<AddEmployeeViewModel> GetAddEmployeeViewModelAsync(AddEmployeeViewModel? vm = null);
        Task DeniedAdvance(string id);
        Task ApproveAdvance(string id);
        Task DeniedExpense(string id);
        Task ApproveExpense(string id);
        Task DeniedPermission(string id);
        Task ApprovePermission(string id);
    }
}
