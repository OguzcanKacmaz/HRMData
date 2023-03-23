using HRMData.Domain.Entities.Common;
using HRMData.Domain.Enums;

namespace HRMData.Domain.Entities
{
    public class EmployeePermissionRequest : BaseEntity
    {
        public ApprovalStatus ApprovalStatus { get; set; }
        public PermissionType PermissionType { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ReplyDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double NumOfDays { get; set; }
        public string? Document { get; set; }
        public string EmployeeId { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
