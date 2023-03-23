using AutoMapper;
using HRMData.Domain.Entities;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;

namespace HRMData.WEB.Areas.HRMPanel.Models.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Employee, EditEmployeeViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeCardViewModel>().ReverseMap();
        }
    }
}
