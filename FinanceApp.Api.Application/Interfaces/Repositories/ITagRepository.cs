using FinanceApp.Api.Application.Repositories.TagRepository.Dto;

namespace FinanceApp.Api.Application.Interfaces.Repositories
{
    public interface ITagRepository
    {
        Task<IList<TagDto>> GetAllTags(Guid userId);
        Task<TagDto?> GetTagById(Guid userId, long id);
        Task<long> CreateTag(CreateTagDto createTag, CancellationToken cancellationToken);
        Task<int> UpdateTag(UpdateTagDto updateTag, CancellationToken cancellationToken);
        Task<int> DeleteTag(Guid userId, long id, CancellationToken cancellationToken);
    }
}
