using HRMData.Domain.Entities;

namespace HRMData.Application.Interfaces
{
    public interface IEmployeeAdvanceRepository : IRepository<EmployeeAdvance>
    {
        Task<List<EmployeeAdvance>> InProgressAdvancesAsync(string EmployeeId);
        Task<List<EmployeeAdvance>> ApprovedAdvancesAsync(string EmployeeId);
        Task<List<EmployeeAdvance>> AllInProgressAdvancesAsync(string EmployeeId);
        Task<List<EmployeeAdvance>> AllApprovedAdvancesAsync(string EmployeeId);
        Task<List<EmployeeAdvance>> AllRejectedAdvancesAsync(string EmployeeId);
        Task<List<EmployeeAdvance>> GetAllAdvancesAsync(string EmployeeId);
        Task<List<EmployeeAdvance>> RejectedAdvancesAsync(string EmployeeId);
        Task<List<EmployeeAdvance>> GetAllOrderByAdvancesAsync(string EmployeeId);
        List<EmployeeAdvance> AdvancePendingRequestsAsync();
    }
}
