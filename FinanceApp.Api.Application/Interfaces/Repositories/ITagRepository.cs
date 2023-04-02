using FinanceApp.Api.Application.Common.Dto;

namespace FinanceApp.Api.Application.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<TagDto>> GetAllTags(Guid userId);
    }
}
