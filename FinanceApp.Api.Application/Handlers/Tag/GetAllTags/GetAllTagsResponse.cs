using FinanceApp.Api.Application.Common.Dto;

namespace FinanceApp.Api.Application.Handlers.Tag.GetAllTags
{
    public class GetAllTagsResponse
    {
        public IEnumerable<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}
