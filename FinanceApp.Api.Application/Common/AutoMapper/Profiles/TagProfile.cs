using AutoMapper;
using FinanceApp.Api.Application.Repositories.TagRepository.Dto;
using FinanceApp.Api.Domain.Models;

namespace FinanceApp.Api.Application.Common.AutoMapper.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
