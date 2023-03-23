namespace HRMData.WEB.Areas.HRMPanel.Models.ViewModels
{
    public class EditEmployeeViewModel
    {
        public string Id { get; set; } = null!;
        public IFormFile? PhotoPath { get; set; }
        public string? Photo { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
