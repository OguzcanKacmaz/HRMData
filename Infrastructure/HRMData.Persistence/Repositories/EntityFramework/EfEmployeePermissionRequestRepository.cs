using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMData.Persistence.Repositories.EntityFramework
{
    public class EfEmployeePermissionRequestRepository : GenericRepository<EmployeePermissionRequest>, IEmployeePermissionRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public EfEmployeePermissionRequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<EmployeePermissionRequest>> ApprovedPermissionRequestAsync(string EmployeeId)
        {
            return await _db.EmployeePermissionRequest.Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Approved && x.EmployeeId == EmployeeId).OrderByDescending(x => x.ReplyDate).ThenByDescending(x => x.RequestDate).ToListAsync();
        }

        public async Task<List<EmployeePermissionRequest>> GetAllOrderByPermissionRequestAsync(string EmployeeId)
        {
            return await _db.EmployeePermissionRequest.Where(x => x.EmployeeId == EmployeeId).OrderBy(x => x.ApprovalStatus).ThenByDescending(x => x.RequestDate).ThenByDescending(x => x.ReplyDate).ToListAsync();
        }

        public async Task<List<EmployeePermissionRequest>> GetApprovedAndInprogressRequestAsync(string EmployeeId)
        {
            var approvedAndInProgress = await _db.EmployeePermissionRequest
                    .Where(x => (x.ApprovalStatus == Domain.Enums.ApprovalStatus.PendingApproval || x.ApprovalStatus == Domain.Enums.ApprovalStatus.Approved) && x.EmployeeId == EmployeeId).ToListAsync();

            return approvedAndInProgress;
        }

        public async Task<List<EmployeePermissionRequest>> InProgressPermissionRequestAsync(string EmployeeId)
        {
            return await _db.EmployeePermissionRequest.Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.PendingApproval && x.EmployeeId == EmployeeId).OrderByDescending(x => x.RequestDate).ThenByDescending(x => x.ReplyDate).ToListAsync();
        }

        public List<EmployeePermissionRequest> PermissionPendingRequests()
        {
            var expensePendingRequests = _db.EmployeePermissionRequest
                   .Where(p => (int)p.ApprovalStatus == 0)
                   .AsEnumerable()
                   .Where(p => (DateTime.Now - p.RequestDate).TotalDays >= 3)
                   .ToList();
            return expensePendingRequests;
        }

        public async Task<List<EmployeePermissionRequest>> RejectedPermissionRequestAsync(string EmployeeId)
        {
            return await _db.EmployeePermissionRequest.Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Denied && x.EmployeeId == EmployeeId).OrderByDescending(x => x.ReplyDate).ThenByDescending(x => x.RequestDate).ToListAsync();
        }

        public async Task<List<EmployeePermissionRequest>> AllApprovedPermissionAsync(string EmployeeId)
        {
            var allAprovedPermission = await _db.EmployeePermissionRequest.Include(x => x.Employee).Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Approved && x.EmployeeId != EmployeeId).OrderByDescending(x => x.ReplyDate).ToListAsync();
            return allAprovedPermission;
        }

        public async Task<List<EmployeePermissionRequest>> AllInProgressPermissionAsync(string EmployeeId)
        {
            var allInProgressPermissionAsync = await _db.EmployeePermissionRequest.Include(x => x.Employee).Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.PendingApproval && x.EmployeeId != EmployeeId).OrderBy(x => x.ReplyDate).ToListAsync();
            return allInProgressPermissionAsync;
        }

        public async Task<List<EmployeePermissionRequest>> AllRejectedPermissionAsync(string EmployeeId)
        {
            var allAprovedPermission = await _db.EmployeePermissionRequest.Include(x => x.Employee).Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Denied && x.EmployeeId != EmployeeId).OrderByDescending(x => x.ReplyDate).ToListAsync();
            return allAprovedPermission;
        }
        public async Task<List<EmployeePermissionRequest>> GetAllPermissionAsync(string EmployeeId)
        {
            var getAllPermissionAsync = await _db.EmployeePermissionRequest.Include(x => x.Employee).Where(x => x.EmployeeId != EmployeeId).OrderByDescending(x => x.ReplyDate).ToListAsync();
            return getAllPermissionAsync;
        }
    }
}
