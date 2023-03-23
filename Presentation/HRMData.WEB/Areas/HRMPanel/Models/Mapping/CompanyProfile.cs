using AutoMapper;
using HRMData.Domain.Entities;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;

namespace HRMData.WEB.Areas.HRMPanel.Models.Mapping
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company,CompanyViewModel>().ReverseMap();
            CreateMap<Company,AddCompanyViewModel>().ReverseMap();
        }
    }
}
