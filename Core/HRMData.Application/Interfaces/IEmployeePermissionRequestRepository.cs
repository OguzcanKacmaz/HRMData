using HRMData.Domain.Entities;

namespace HRMData.Application.Interfaces
{
    public interface IEmployeePermissionRequestRepository : IRepository<EmployeePermissionRequest>
    {
        Task<List<EmployeePermissionRequest>> InProgressPermissionRequestAsync(string EmployeeId);
        Task<List<EmployeePermissionRequest>> ApprovedPermissionRequestAsync(string EmployeeId);
        Task<List<EmployeePermissionRequest>> RejectedPermissionRequestAsync(string EmployeeId);
        Task<List<EmployeePermissionRequest>> GetAllOrderByPermissionRequestAsync(string EmployeeId);
        Task<List<EmployeePermissionRequest>> GetApprovedAndInprogressRequestAsync(string EmployeeId);
        List<EmployeePermissionRequest> PermissionPendingRequests();
        Task<List<EmployeePermissionRequest>> AllInProgressPermissionAsync(string EmployeeId);
        Task<List<EmployeePermissionRequest>> AllApprovedPermissionAsync(string EmployeeId);
        Task<List<EmployeePermissionRequest>> AllRejectedPermissionAsync(string EmployeeId);
        Task<List<EmployeePermissionRequest>> GetAllPermissionAsync(string EmployeeId);
    }
}
