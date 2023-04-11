using FinanceApp.Api.Application.Repositories.TagRepository.Dto;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.GetAllTagsHandler
{
    public class GetAllTagsResponse
    {
        public IList<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}
