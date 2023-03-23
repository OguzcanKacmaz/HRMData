using AutoMapper;
using HRMData.Application.Interfaces;
using HRMData.Domain.Entities;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMData.WEB.Areas.HRMPanel.ViewComponents
{
    public class RightUserViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository _EmployeeRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public RightUserViewComponent(IEmployeeRepository EmployeeRepo, IMapper mapper, UserManager<AppUser> userManager)
        {
            _EmployeeRepo = EmployeeRepo;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var Employee = await _EmployeeRepo.GetUserEmployeeAsync(user.Id);
            return View(_mapper.Map<EmployeeViewModel>(await _EmployeeRepo.GetByIdAsync(Employee.Id)));
        }
    }
}
