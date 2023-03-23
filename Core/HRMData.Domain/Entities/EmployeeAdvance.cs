using HRMData.Domain.Entities.Common;
using HRMData.Domain.Enums;

namespace HRMData.Domain.Entities
{
    public class EmployeeAdvance : BaseEntity
    {
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime? ReplyDate { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public AdvancePaymentType AdvancePaymentType { get; set; }
        public string Description { get; set; } = null!;
        public string EmployeeId { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
