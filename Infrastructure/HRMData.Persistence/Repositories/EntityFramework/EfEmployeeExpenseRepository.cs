using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Data;
using Microsoft.EntityFrameworkCore;

namespace HRMData.Persistence.Repositories.EntityFramework
{
    public class EfEmployeeExpenseRepository : GenericRepository<EmployeeExpense>, IEmployeeExpenseRepository
    {
        private readonly ApplicationDbContext _db;

        public EfEmployeeExpenseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<EmployeeExpense>> ApprovedExpenseAsync(string EmployeeId)
        {
            return await _db.EmployeeExpenses.Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Approved && x.EmployeeId == EmployeeId).OrderByDescending(x => x.ReplyDate).ThenByDescending(x => x.RequestDate).ToListAsync();
        }

        public List<EmployeeExpense> ExpensePendingRequests()
        {
            var expensePendingRequests = _db.EmployeeExpenses
                   .Where(p => (int)p.ApprovalStatus == 0)
                   .AsEnumerable()
                   .Where(p => (DateTime.Now - p.RequestDate).TotalDays >= 3)
                   .ToList();
            return expensePendingRequests;
        }

        public async Task<List<EmployeeExpense>> GetAllOrderByExpenseAsync(string EmployeeId)
        {
            return await _db.EmployeeExpenses.Where(x => x.EmployeeId == EmployeeId).OrderBy(x => x.ApprovalStatus).ThenByDescending(x => x.RequestDate).ThenByDescending(x => x.ReplyDate).ToListAsync();
        }

        public async Task<List<EmployeeExpense>> InProgressExpenseAsync(string EmployeeId)
        {
            return await _db.EmployeeExpenses.Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.PendingApproval && x.EmployeeId == EmployeeId).OrderByDescending(x => x.RequestDate).ThenByDescending(x => x.ReplyDate).ToListAsync();
        }

        public async Task<List<EmployeeExpense>> RejectedExpenseAsync(string EmployeeId)
        {
            return await _db.EmployeeExpenses.Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Denied && x.EmployeeId == EmployeeId).OrderByDescending(x => x.ReplyDate).ThenByDescending(x => x.RequestDate).ToListAsync();
        }
        public async Task<List<EmployeeExpense>> AllApprovedExpenseAsync(string EmployeeId)
        {
            var allAprovedExpense = await _db.EmployeeExpenses.Include(x => x.Employee).Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Approved && x.EmployeeId != EmployeeId).OrderByDescending(x => x.ReplyDate).ToListAsync();
            return allAprovedExpense;
        }

        public async Task<List<EmployeeExpense>> AllInProgressExpenseAsync(string EmployeeId)
        {
            var allInProgressExpenseAsync = await _db.EmployeeExpenses.Include(x => x.Employee).Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.PendingApproval && x.EmployeeId != EmployeeId).OrderBy(x => x.ReplyDate).ToListAsync();
            return allInProgressExpenseAsync;
        }

        public async Task<List<EmployeeExpense>> AllRejectedExpenseAsync(string EmployeeId)
        {
            var allAprovedExpense = await _db.EmployeeExpenses.Include(x => x.Employee).Where(x => x.ApprovalStatus == Domain.Enums.ApprovalStatus.Denied && x.EmployeeId != EmployeeId).OrderByDescending(x => x.ReplyDate).ToListAsync();
            return allAprovedExpense;
        }
        public async Task<List<EmployeeExpense>> GetAllExpenseAsync(string EmployeeId)
        {
            var getAllExpenseAsync = await _db.EmployeeExpenses.Include(x => x.Employee).Where(x => x.EmployeeId != EmployeeId).OrderByDescending(x => x.ReplyDate).ToListAsync();
            return getAllExpenseAsync;
        }
    }
}
