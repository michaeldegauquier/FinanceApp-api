using AutoMapper;
using FinanceApp.Api.Application.Common.AutoMapper.Profiles;

namespace FinanceApp.Api.Application.Common.AutoMapper
{
    public static class Mapper
    {
        private static readonly IMapper _mapper;

        static Mapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<IncomeExpenseProfile>();
                cfg.AddProfile<TagProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public static TDestination? Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public static IList<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> sourceList)
        {
            if (sourceList == null)
                return new List<TDestination>();

            var result = _mapper.Map<List<TDestination>>(sourceList);

            if (result == null)
                return new List<TDestination>();
            return result;
        }
    }
}
