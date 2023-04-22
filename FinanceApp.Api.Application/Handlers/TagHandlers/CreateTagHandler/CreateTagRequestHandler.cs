using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Application.Repositories.TagRepo.Dto;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.CreateTagHandler
{
    public class CreateTagRequestHandler : IRequestHandler<CreateTagRequest, DataResponse<CreateTagResponse>>
    {
        private readonly IUserService _userService;
        private readonly ITagRepository _tagRepository;

        public CreateTagRequestHandler(IUserService userService, ITagRepository tagRepository)
        {
            _userService = userService;
            _tagRepository = tagRepository;
        }

        public async Task<DataResponse<CreateTagResponse>> Handle(CreateTagRequest request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();

            if (userId == Guid.Empty)
                return ResponseFactory.Error<CreateTagResponse>(ErrorType.UserIdNotFound);

            var result = await _tagRepository.CreateTag(new CreateTagDto
            {
                UserId = userId,
                Name = request.Name,
            }, cancellationToken);
            return CreateResponse(result);
        }

        private static DataResponse<CreateTagResponse> CreateResponse(long result)
        {
            if (result == -1)
                return ResponseFactory.Error<CreateTagResponse>(ErrorType.DuplicateCreateItem);
            return ResponseFactory.Success(GetResponse(result), SuccessType.DataFound);
        }

        private static CreateTagResponse GetResponse(long id)
        {
            return new CreateTagResponse
            {
                Id = id
            };
        }
    }
}
