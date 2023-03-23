using HRMData.Domain.Enums;

namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class AddEmployeeAdvanceViewModel
    {
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.PendingApproval;
        public DateTime? ReplyDate { get; set; }
        public decimal Amount { get; set; }
        public Currency? Currency { get; set; }
        public AdvancePaymentType? AdvancePaymentType { get; set; }
        public string? Description { get; set; }
    }
}
