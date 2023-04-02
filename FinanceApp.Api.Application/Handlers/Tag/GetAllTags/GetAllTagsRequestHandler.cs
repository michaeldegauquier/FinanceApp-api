using FinanceApp.Api.Application.Common.Dto;
using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.Tag.GetAllTags
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

            var incomesExpenses = await _tagRepository.GetAllTags(userId);
            return ResponseFactory.Success(GetResponse(incomesExpenses), SuccessType.DataFound);
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
