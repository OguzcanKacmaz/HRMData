using AutoMapper;
using HRMData.Domain.Entities;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;

namespace HRMData.WEB.Areas.HRMPanel.Models.Mapping
{
    public class PermissionRequestProfile : Profile
    {
        public PermissionRequestProfile()
        {
            CreateMap<EmployeePermissionRequest, AddEmployeePermissionRequestViewModel>().ReverseMap();
            CreateMap<PermissionRequestNumOfDaysViewModel, PermissionRequestNumOfDays>().ReverseMap();
        }
    }
}
