using HRMData.Domain.Enums;

namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class AddEmployeePermissionRequestViewModel
    {
        public PermissionType? PermissionType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double NumOfDays { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; } = ApprovalStatus.PendingApproval;
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public string? Document { get; set; }
        public IFormFile? DocumentPath { get; set; }

    }
}
