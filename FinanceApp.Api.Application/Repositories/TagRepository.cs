using FinanceApp.Api.Application.Interfaces;
using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IApplicationDbContext _context;

        public TagRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Tag>> GetAllTags(string userId)
        {
            return await _context.Tags
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Name)
                .ToListAsync();
        }
    }
}
