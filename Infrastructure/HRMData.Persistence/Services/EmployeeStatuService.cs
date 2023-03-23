using HRMData.Application.Interfaces;

namespace HRMData.Persistence.Services
{
    public class EmployeeStatuService : IEmployeeStatuService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;

        public EmployeeStatuService(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
        }
        public async Task EmployeeStatuCheck()
        {
            var employees = await _employeeRepository.GetAllAsync();
            foreach (var employee in employees)
            {
                var company = await _companyRepository.GetByIdAsync(employee.CompanyId!);

                if (employee.HireDate <= DateTime.Now && employee.IsActive == Domain.Entities.Enums.Status.Passive && employee.TerminationDate == null && company!.IsActive == Domain.Entities.Enums.Status.Active)
                {
                    employee.IsActive = Domain.Entities.Enums.Status.Active;
                }
                if (employee.HireDate <= DateTime.Now && employee.IsActive == Domain.Entities.Enums.Status.Passive && employee.TerminationDate > DateTime.Now && company!.IsActive == Domain.Entities.Enums.Status.Active)
                {
                    employee.IsActive = Domain.Entities.Enums.Status.Active;
                }

                if (employee.IsActive == Domain.Entities.Enums.Status.Active && company!.IsActive == Domain.Entities.Enums.Status.Passive)
                {
                    employee.IsActive = Domain.Entities.Enums.Status.Passive;                    
                }

                if (employee.IsActive == Domain.Entities.Enums.Status.Active && company!.IsActive == Domain.Entities.Enums.Status.Active && employee.TerminationDate <= DateTime.Now)
                {
                    employee.IsActive = Domain.Entities.Enums.Status.Passive;
                    company.EmployeeCount++;
                }
                await _employeeRepository.UpdateAsync(employee);
                await _companyRepository.UpdateAsync(company!);
            }
        }
    }
}
