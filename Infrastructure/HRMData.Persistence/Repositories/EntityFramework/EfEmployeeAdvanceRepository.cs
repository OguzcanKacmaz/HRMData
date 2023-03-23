using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMData.Persistence.Repositories.EntityFramework
{
    public class EfEmployeeAdvanceRepository : GenericRepository<EmployeeAdvance>, IEmployeeAdvanceRepository
    {
        private readonly ApplicationDbContext _db;

        public EfEmployeeAdvanceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public List<EmployeeAdvance> AdvancePendingRequestsAsync()
        {
            var advancePendingRequests = _db.EmployeeAdvances.Where(p => (int)p.ApprovalStatus == 0).AsEnumerable().Where(p => (DateTime.Now - p.RequestDate).TotalDays >= 3).ToList();
            return advancePendingRequests;
        }

        public async Task<List<EmployeeAdvance>> AllApprovedAdvancesAsync(string EmployeeId)
        {
            var allAprovedAdvances = await _db.EmployeeAdvances.Include(x=>x.Employee).Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Approved && x.EmployeeId != EmployeeId).OrderByDescending(x => x.ReplyDate).ToListAsync();
            return allAprovedAdvances;
        }

        public async Task<List<EmployeeAdvance>> AllInProgressAdvancesAsync(string EmployeeId)
        {
            var allInProgressAdvancesAsync = await _db.EmployeeAdvances.Include(x => x.Employee).Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.PendingApproval && x.EmployeeId != EmployeeId).OrderBy(x => x.ReplyDate).ToListAsync();
            return allInProgressAdvancesAsync;
        }

        public async Task<List<EmployeeAdvance>> AllRejectedAdvancesAsync(string EmployeeId)
        {
            var allAprovedAdvances = await _db.EmployeeAdvances.Include(x => x.Employee).Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Denied && x.EmployeeId != EmployeeId).OrderByDescending(x => x.ReplyDate).ToListAsync();
            return allAprovedAdvances;
        }
        public async Task<List<EmployeeAdvance>> GetAllAdvancesAsync(string EmployeeId)
        {
            var getAllAdvancesAsync = await _db.EmployeeAdvances.Include(x => x.Employee).Where(x=> x.EmployeeId != EmployeeId).OrderByDescending(x => x.ReplyDate).ToListAsync();
            return getAllAdvancesAsync;
        }

        public async Task<List<EmployeeAdvance>> ApprovedAdvancesAsync(string EmployeeId)
        {
            return await _db.EmployeeAdvances.Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Approved && x.EmployeeId == EmployeeId).OrderByDescending(x => x.ReplyDate).ThenByDescending(x => x.RequestDate).ToListAsync();

        }


        public async Task<List<EmployeeAdvance>> GetAllOrderByAdvancesAsync(string EmployeeId)
        {
            return await _db.EmployeeAdvances.Where(x => x.EmployeeId == EmployeeId).OrderBy(x => x.ApprovalStatus).ThenByDescending(x => x.RequestDate).ThenByDescending(x => x.ReplyDate).ToListAsync();

        }

        public async Task<List<EmployeeAdvance>> InProgressAdvancesAsync(string EmployeeId)
        {
            return await _db.EmployeeAdvances.Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.PendingApproval && x.EmployeeId == EmployeeId).OrderByDescending(x => x.RequestDate).ThenByDescending(x => x.ReplyDate).ToListAsync();
        }

        public async Task<List<EmployeeAdvance>> RejectedAdvancesAsync(string EmployeeId)
        {
            return await _db.EmployeeAdvances.Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Denied && x.EmployeeId == EmployeeId).OrderByDescending(x => x.ReplyDate).ThenByDescending(x => x.RequestDate).ToListAsync();
        }
    }
}
