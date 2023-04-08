using FinanceApp.Api.Application.Common.AutoMapper;
using FinanceApp.Api.Application.Interfaces;
using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Repositories.TagRepository.Dto;
using FinanceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.TagRepository
{
    public class TagRepository : ITagRepository
    {
        private readonly IApplicationDbContext _context;

        public TagRepository(IApplicationDbContext context)
        {
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

            return Mapper.MapList<Tag, TagDto>(result);
        }

        public async Task<TagDto?> GetTagById(Guid userId, long id)
        {
            var result = await _context.Tags
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Name)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                return null;

            return Mapper.Map<Tag, TagDto?>(result);
        }

        public async Task<long> CreateTag(CreateTagDto createTag, CancellationToken cancellationToken)
        {
            var tagToCreate = new Tag
            {
                UserId = createTag.UserId,
                Name = createTag.Name
            };

            await _context.Tags.AddAsync(tagToCreate);
            await _context.SaveChangesAsync(cancellationToken);

            return tagToCreate.Id;
        }

        public async Task<int> UpdateTag(UpdateTagDto updateTag, CancellationToken cancellationToken)
        {
            var tag = await _context.Tags
                .Where(x => x.UserId == updateTag.UserId)
                .FirstOrDefaultAsync(x => x.Id == updateTag.Id);

            if (tag == null)
                return -1;

            tag.Name = updateTag.Name;

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result;
        }

        public async Task<int> DeleteTag(Guid userId, long id, CancellationToken cancellationToken)
        {
            var tag = await _context.Tags
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tag == null)
                return -1;

            _context.Tags.Remove(tag);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
