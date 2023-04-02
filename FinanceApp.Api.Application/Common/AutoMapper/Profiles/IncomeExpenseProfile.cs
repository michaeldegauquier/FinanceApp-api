using AutoMapper;
using FinanceApp.Api.Application.Common.Dto;
using FinanceApp.Api.Domain.Models;

namespace FinanceApp.Api.Application.Common.AutoMapper.Profiles
{
    public class IncomeExpenseProfile : Profile
    {
        public IncomeExpenseProfile()
        {
            CreateMap<IncomeExpense, IncomeExpenseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.IsIncome, opt => opt.MapFrom(src => src.IsIncome))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));
        }
    }
}
