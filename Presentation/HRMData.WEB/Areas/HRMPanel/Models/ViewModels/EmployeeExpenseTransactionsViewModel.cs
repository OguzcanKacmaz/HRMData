using HRMData.Domain.Entities;

namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class EmployeeExpenseTransactionsViewModel
    {
        public List<EmployeeExpense> AllExpenses { get; set; } = new();
        public List<EmployeeExpense> InProgressExpenses { get; set; } = new();
        public List<EmployeeExpense> ApprovedExpenses { get; set; } = new();
        public List<EmployeeExpense> RejectedExpenses { get; set; } = new();
    }
}
