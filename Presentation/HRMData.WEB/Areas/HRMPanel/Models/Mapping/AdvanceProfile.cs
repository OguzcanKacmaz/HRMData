using AutoMapper;
using HRMData.Domain.Entities;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;

namespace HRMData.WEB.Areas.HRMPanel.Models.Mapping
{
    public class AdvanceProfile : Profile
    {
        public AdvanceProfile()
        {
            CreateMap<EmployeeAdvance, AddEmployeeAdvanceViewModel>().ReverseMap();
        }
    }
}
