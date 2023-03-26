using FinanceApp.Api.Domain.Models;

namespace FinanceApp.Api.Application.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task<ICollection<Tag>> GetAllTags(string userId);
    }
}
