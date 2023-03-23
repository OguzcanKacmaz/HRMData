using AutoMapper;
using HRMData.Domain.Entities;
using HRMData.WEB.Areas.HRMPanel.Models.ViewModels;

namespace HRMData.WEB.Areas.HRMPanel.Models.Mapping
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<EmployeeExpense, AddEmployeeExpenseViewModel>().ReverseMap();
        }
    }
}
