using FinanceApp.Api.Application.Repositories.TagRepository.Dto;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.GetAllTagsHandler
{
    public class GetAllTagsResponse
    {
        public IEnumerable<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}
