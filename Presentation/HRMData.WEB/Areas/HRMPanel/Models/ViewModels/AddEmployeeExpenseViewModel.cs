using HRMData.Domain.Enums;

namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class AddEmployeeExpenseViewModel
    {
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.PendingApproval;
        public ExpenseType ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public Currency? Currency { get; set; }
        public IFormFile? ExpenseDocumentPath { get; set; }
        public string? ExpenseDocument { get; set; }
    }
}
