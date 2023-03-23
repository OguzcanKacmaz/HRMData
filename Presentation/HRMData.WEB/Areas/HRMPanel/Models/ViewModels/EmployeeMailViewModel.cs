using HRMData.Domain.Entities;

namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class EmployeeMailViewModel
    {
        public AppUser? Employee { get; set; }
        public string? Password { get; set; }
    }
}
