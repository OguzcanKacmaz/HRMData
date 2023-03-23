using HRMData.Domain.Entities;

namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class EmployeeAdvanceTransactionsViewModel
    {
        public List<EmployeeAdvance> AllAdvances { get; set; } = new();
        public List<EmployeeAdvance> InProgressAdvances { get; set; } = new();
        public List<EmployeeAdvance> ApprovedAdvances { get; set; } = new();
        public List<EmployeeAdvance> RejectedAdvances { get; set; } = new();
        
    }
}
