using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMData.Persistence.Services
{
    public class CompanyStatuService : ICompanyStatuCheckService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public CompanyStatuService(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository)
        {
            _companyRepository = companyRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task CompanyStatuCheck()
        {
            var companies = await _companyRepository.GetAllIncludeEmployees();
            foreach (var company in companies)
            {
                if (company.IsActive == Domain.Entities.Enums.Status.Passive)
                {
                    if (company.Employees.Count != 0)
                    {
                        foreach (var employee in company.Employees)
                        {
                            employee.IsActive = Domain.Entities.Enums.Status.Passive;
                            await _employeeRepository.UpdateAsync(employee);
                        }
                    }
                    await _companyRepository.UpdateAsync(company);
                }
                if (company.IsActive == Domain.Entities.Enums.Status.Active)
                {
                    if (company.Employees.Count != 0)
                    {
                        foreach (var employee in company.Employees)
                        {                            
                            if ((employee.TerminationDate == null && employee.HireDate <= DateTime.Now) || (employee.TerminationDate >= DateTime.Now && employee.HireDate <= DateTime.Now))
                            {
                                employee.IsActive = Domain.Entities.Enums.Status.Active;
                            }
                            await _employeeRepository.UpdateAsync(employee);
                        }
                    }
                    await _companyRepository.UpdateAsync(company);
                }
                if (company.ContractEndDate < DateTime.Now)
                {
                    company.IsActive = Domain.Entities.Enums.Status.Passive;
                    if (company.Employees.Count != 0)
                    {
                        foreach (var employee in company.Employees)
                        {
                            employee.IsActive = Domain.Entities.Enums.Status.Passive;
                            await _employeeRepository.UpdateAsync(employee);
                        }
                    }
                    await _companyRepository.UpdateAsync(company);
                }
                if (company.ContractEndDate > DateTime.Now && company.IsActive==Domain.Entities.Enums.Status.Passive)
                {
                    company.IsActive = Domain.Entities.Enums.Status.Active;
                    if (company.Employees.Count != 0)
                    {
                        foreach (var employee in company.Employees)
                        {
                            if ((employee.TerminationDate == null && employee.HireDate <= DateTime.Now) || (employee.TerminationDate >= DateTime.Now && employee.HireDate <= DateTime.Now))
                            {
                                employee.IsActive = Domain.Entities.Enums.Status.Active;
                            }
                            await _employeeRepository.UpdateAsync(employee);
                        }
                    }
                    await _companyRepository.UpdateAsync(company);
                }
            }
        }
    }
}
