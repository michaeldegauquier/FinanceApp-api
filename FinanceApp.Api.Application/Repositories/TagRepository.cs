using AutoMapper;
using FinanceApp.Api.Application.Common.Dto;
using FinanceApp.Api.Application.Interfaces;
using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public TagRepository(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<TagDto>> GetAllTags(Guid userId)
        {
            var result = await _context.Tags
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Name)
                .ToListAsync();

            if (result == null)
                return new List<TagDto>();

            return GetResult(result);
        }

        private List<TagDto> GetResult(List<Tag> tags)
        {
            var result = _mapper.Map<List<TagDto>>(tags);

            if (result == null)
                return new List<TagDto>();
            return result;
        }
    }
}
