using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Application.Repositories.TagRepository.Dto;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.GetAllTagsHandler
{
    public class GetAllTagsRequestHandler : IRequestHandler<GetAllTagsRequest, DataResponse<GetAllTagsResponse>>
    {
        private readonly IUserService _userService;
        private readonly ITagRepository _tagRepository;

        public GetAllTagsRequestHandler(IUserService userService, ITagRepository tagRepository)
        {
            _userService = userService;
            _tagRepository = tagRepository;
        }

        public async Task<DataResponse<GetAllTagsResponse>> Handle(GetAllTagsRequest request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();

            if (userId == Guid.Empty)
                return ResponseFactory.Error<GetAllTagsResponse>(ErrorType.UserIdNotFound);

            var tags = await _tagRepository.GetAllTags(userId);
            return ResponseFactory.Success(GetResponse(tags), SuccessType.DataFound);
        }

        private static GetAllTagsResponse GetResponse(IEnumerable<TagDto> tags)
        {
            return new GetAllTagsResponse
            {
                Tags = tags
            };
        }
    }
}
