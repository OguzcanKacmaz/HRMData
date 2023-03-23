using HRMData.Domain.Entities;

namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class EmployeePermissionRequestTransactionsViewModel
    {
        public List<EmployeePermissionRequest> AllPermissionRequest { get; set; } = new();
        public List<EmployeePermissionRequest> InProgressPermissionRequest { get; set; } = new();
        public List<EmployeePermissionRequest> ApprovedPermissionRequest { get; set; } = new();
        public List<EmployeePermissionRequest> RejectedPermissionRequest { get; set; } = new();
    }
}
