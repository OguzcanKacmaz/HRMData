using HRMData.Domain.Entities.Common;
using HRMData.Domain.Enums;

namespace HRMData.Domain.Entities
{
    public class EmployeeExpense : BaseEntity
    {
        public ExpenseType ExpenseType { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime? ReplyDate { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string ExpenseDocument { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
