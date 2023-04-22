using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Application.Repositories.TagRepo.Dto;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.GetTagByIdHandler
{
    public class GetTagByIdRequestHandler : IRequestHandler<GetTagByIdRequest, DataResponse<GetTagByIdResponse>>
    {
        private readonly IUserService _userService;
        private readonly ITagRepository _tagRepository;

        public GetTagByIdRequestHandler(IUserService userService, ITagRepository tagRepository)
        {
            _userService = userService;
            _tagRepository = tagRepository;
        }

        public async Task<DataResponse<GetTagByIdResponse>> Handle(GetTagByIdRequest request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();

            if (userId == Guid.Empty)
                return ResponseFactory.Error<GetTagByIdResponse>(ErrorType.UserIdNotFound);

            var tag = await _tagRepository.GetTagById(userId, request.Id);
            return CreateResponse(tag);
        }

        private static DataResponse<GetTagByIdResponse> CreateResponse(TagDto? tag)
        {
            if (tag == null)
                return ResponseFactory.Error<GetTagByIdResponse>(ErrorType.ItemNotFound);

            return ResponseFactory.Success(GetResponse(tag), SuccessType.DataFound);
        }

        private static GetTagByIdResponse GetResponse(TagDto? tag)
        {
            return new GetTagByIdResponse
            {
                Tag = tag
            };
        }
    }
}
