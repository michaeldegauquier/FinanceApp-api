using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Application.Repositories.TagRepo.Dto;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.UpdateTagHandler
{
    public class UpdateTagRequestHandler : IRequestHandler<UpdateTagRequest, DataResponse<UpdateTagResponse>>
    {
        private readonly IUserService _userService;
        private readonly ITagRepository _tagRepository;

        public UpdateTagRequestHandler(IUserService userService, ITagRepository tagRepository)
        {
            _userService = userService;
            _tagRepository = tagRepository;
        }

        public async Task<DataResponse<UpdateTagResponse>> Handle(UpdateTagRequest request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();

            if (userId == Guid.Empty)
                return ResponseFactory.Error<UpdateTagResponse>(ErrorType.UserIdNotFound);

            var result = await _tagRepository.UpdateTag(new UpdateTagDto
            {
                UserId = userId,
                Id = request.Id,
                Name = request.Name,
            }, cancellationToken);
            return CreateResponse(result);
        }

        private static DataResponse<UpdateTagResponse> CreateResponse(int result)
        {
            if (result == -1)
                return ResponseFactory.Error<UpdateTagResponse>(ErrorType.DuplicateUpdateItem);
            else if (result == -2)
                return ResponseFactory.Error<UpdateTagResponse>(ErrorType.FailedToUpdate);
            return ResponseFactory.Success(GetResponse(result), SuccessType.UpdatedItem);
        }

        private static UpdateTagResponse GetResponse(int updatedItems)
        {
            return new UpdateTagResponse
            {
                UpdatedItems = updatedItems
            };
        }
    }
}
