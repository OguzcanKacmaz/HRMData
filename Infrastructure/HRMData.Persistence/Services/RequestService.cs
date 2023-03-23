using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Data;

namespace HRMData.Persistence.Services
{
    public class RequestService : IAdvanceRequestService
    {
        private readonly IEmployeeAdvanceRepository _employeeAdvanceRepository;
        private readonly IEmployeeExpenseRepository _employeeExpenseRepository;
        private readonly ApplicationDbContext _db;
        private readonly IEmployeeRepository _EmployeeRepository;

        public RequestService(IEmployeeAdvanceRepository employeeAdvanceRepository, IEmployeeExpenseRepository employeeExpenseRepository, ApplicationDbContext db, IEmployeeRepository EmployeeRepository)
        {
            _employeeAdvanceRepository = employeeAdvanceRepository;
            _employeeExpenseRepository = employeeExpenseRepository;
            _db = db;
            _EmployeeRepository = EmployeeRepository;
        }
        public async Task DeleteExpiredAdvanceRequests()
        {
            var advancePendingRequests = _employeeAdvanceRepository.AdvancePendingRequestsAsync();
            var expensePendingRequests = _employeeExpenseRepository.ExpensePendingRequests();

            if (advancePendingRequests.Any())
            {
                foreach (var advance in advancePendingRequests)
                {
                    await _employeeAdvanceRepository.DeleteAsync(advance);
                }
            }
            if (expensePendingRequests.Any())
            {
                foreach (var expense in expensePendingRequests)
                {
                    await _employeeExpenseRepository.DeleteAsync(expense);
                }
            }

        }
        public async Task AnnualLeaveRenewalAsync()
        {
            var Employees = _db.Employees.Where(x => x.IsActive == Domain.Entities.Enums.Status.Active).AsEnumerable().Where(p => (DateTime.Now - p.HireDate).TotalDays >= 365).ToList();
            foreach (var Employee in Employees)
            {
                if ((DateTime.Now - Employee.HireDate).TotalDays < 365)
                {
                    Employee.AnnualLeaveDays = 0;
                    Employee.RemainingAnnualLeaveDays = 0;
                }
                if ((DateTime.Now - Employee.HireDate).TotalDays >= 365 && (DateTime.Now - Employee.HireDate).TotalDays <= 1825)
                {
                    Employee.AnnualLeaveDays = 14;
                }

                else
                    Employee.AnnualLeaveDays = 20;

                await _EmployeeRepository.UpdateAsync(Employee);
            }
        }
        public async Task ResetDateAsync()
        {
            var Employees = _db.Employees.AsEnumerable().Where(x => x.IsActive == Domain.Entities.Enums.Status.Active).ToList();
            foreach (var Employee in Employees)
            {
                if ((DateTime.Now - Employee.ResetDate).TotalDays >= 0)
                {
                    Employee.ResetDate = Employee.ResetDate.AddYears(1);
                    Employee.MaxPaymentAmount = Employee.Salary * 3;
                    Employee.RemainingAdvanceAmount = Employee.MaxPaymentAmount;
                    Employee.RemainingAnnualLeaveDays += Employee.AnnualLeaveDays;
                    Employee.ExcusedAbsenceDay = 1;
                    await _EmployeeRepository.UpdateAsync(Employee);
                }
            }
        }

    }
}
