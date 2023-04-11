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

        /// <summary>
        /// Get all tags for logged-in user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of tags</returns>
        public async Task<IList<TagDto>> GetAllTags(Guid userId)
        {
            var result = await _context.Tags
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Name)
                .ToListAsync();

            return Mapper.MapList<Tag, TagDto>(result);
        }

        /// <summary>
        /// Get one single tag for logged-in user by tagId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns>Tag</returns>
        public async Task<TagDto?> GetTagById(Guid userId, long id)
        {
            var result = await _context.Tags
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Name)
                .FirstOrDefaultAsync(x => x.Id == id);

            return Mapper.Map<Tag?, TagDto?>(result);
        }

        /// <summary>
        /// Create one single tag for logged-in user
        /// </summary>
        /// <param name="createTag"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>TagId</returns>
        public async Task<long> CreateTag(CreateTagDto createTag, CancellationToken cancellationToken)
        {
            var duplicateTag = await _context.Tags
                .Where(x => x.UserId == createTag.UserId && 
                            x.Name.ToLower() == createTag.Name.ToLower().Trim())
                .FirstOrDefaultAsync();
            
            if (duplicateTag != null)
                return -1;

            var tagToCreate = new Tag
            {
                UserId = createTag.UserId,
                Name = createTag.Name.Trim()
            };

            await _context.Tags.AddAsync(tagToCreate);
            await _context.SaveChangesAsync(cancellationToken);

            return tagToCreate.Id;
        }

        /// <summary>
        /// Update one single tag for logged-in user
        /// </summary>
        /// <param name="updateTag"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Amount updated</returns>
        public async Task<int> UpdateTag(UpdateTagDto updateTag, CancellationToken cancellationToken)
        {
            var duplicateTag = await _context.Tags
                .Where(x => x.UserId == updateTag.UserId &&
                            x.Name.ToLower() == updateTag.Name.ToLower().Trim() &&
                            x.Id != updateTag.Id)
                .FirstOrDefaultAsync();

            if (duplicateTag != null)
                return -1;

            var tag = await _context.Tags
                .Where(x => x.UserId == updateTag.UserId)
                .FirstOrDefaultAsync(x => x.Id == updateTag.Id);

            if (tag == null)
                return -2;

            tag.Name = updateTag.Name.Trim();

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result;
        }

        /// <summary>
        /// Delete one single tag for logged-in user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Amount deleted</returns>
        public async Task<int> DeleteTag(Guid userId, long id, CancellationToken cancellationToken)
        {
            var tag = await _context.Tags
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (tag == null)
                return -1;

            var tagsToRemove = await _context.IncomeExpenseTags
                .Where(x => x.TagId == id)
                .ToListAsync();

            foreach (var tagToRemove in tagsToRemove)
                _context.IncomeExpenseTags.Remove(tagToRemove);

            _context.Tags.Remove(tag);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
