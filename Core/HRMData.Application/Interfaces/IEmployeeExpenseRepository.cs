using HRMData.Domain.Entities;

namespace HRMData.Application.Interfaces
{
    public interface IEmployeeExpenseRepository : IRepository<EmployeeExpense>
    {
        Task<List<EmployeeExpense>> InProgressExpenseAsync(string EmployeeId);
        Task<List<EmployeeExpense>> ApprovedExpenseAsync(string EmployeeId);
        Task<List<EmployeeExpense>> RejectedExpenseAsync(string EmployeeId);
        Task<List<EmployeeExpense>> GetAllOrderByExpenseAsync(string EmployeeId);
        List<EmployeeExpense> ExpensePendingRequests();
        Task<List<EmployeeExpense>> AllInProgressExpenseAsync(string EmployeeId);
        Task<List<EmployeeExpense>> AllApprovedExpenseAsync(string EmployeeId);
        Task<List<EmployeeExpense>> AllRejectedExpenseAsync(string EmployeeId);
        Task<List<EmployeeExpense>> GetAllExpenseAsync(string EmployeeId);
    }
}
