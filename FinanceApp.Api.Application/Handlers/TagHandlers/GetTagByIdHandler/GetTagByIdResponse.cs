using FinanceApp.Api.Application.Repositories.TagRepository.Dto;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.GetTagByIdHandler
{
    public class GetTagByIdResponse
    {
        public TagDto? Tag { get; set; } = new TagDto();
    }
}
