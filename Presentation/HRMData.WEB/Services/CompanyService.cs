using AutoMapper;
using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;
using HRMData.WEB.Interfaces;
using HRMData.WEB.Models;
using System.Text.RegularExpressions;

namespace HRMData.WEB.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, IWebHostEnvironment env)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task AddCompanyAsync(AddCompanyViewModel vm)
        {
            vm.LogoMini = ImageFileOperations.ResizeAddPhoto(vm.LogoPath!, 32, _env, "img/logo");
            vm.Logo = ImageFileOperations.ResizeAddPhoto(vm.LogoPath!, 230, _env, "img/logo");
            vm.Name = TrimmedText(vm.Name!);
            vm.TaxAdministration=TrimmedText(vm.TaxAdministration!);
            var company = _mapper.Map<Company>(vm);
            if (vm.ContractStartDate > DateTime.Now)
                company.IsActive = Domain.Entities.Enums.Status.Passive;
            if (vm.ContractStartDate <= DateTime.Now && vm.ContractEndDate > DateTime.Now)
                company.IsActive = Domain.Entities.Enums.Status.Active;
            else
                company.IsActive = Domain.Entities.Enums.Status.Passive;
            await _companyRepository.AddAsync(company);
        }

        public async Task<bool> CompanyEmployeeCountCheck(string companyId)
        {
            var company = await _companyRepository.GetByIdAsync(companyId);
            if (company!.EmployeeCount == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<List<CompanyViewModel>> GetAllCompanies()
        {
            var companyViewModel = new List<CompanyViewModel>();
            var companies = await _companyRepository.GetAllAsync();
            foreach (var company in companies)
            {
                var companyMap = _mapper.Map<CompanyViewModel>(company);
                companyViewModel.Add(companyMap);
            }
            return companyViewModel;
        }

        private string TrimmedText(string text)
        {
            string trimmedText = Regex.Replace(text.Trim(), @"\s+", " ");
            return trimmedText;
        }
    }
}
