using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.DeleteTagHandler
{
    public class DeleteTagRequestHandler : IRequestHandler<DeleteTagRequest, DataResponse<DeleteTagResponse>>
    {
        private readonly IUserService _userService;
        private readonly ITagRepository _tagRepository;

        public DeleteTagRequestHandler(IUserService userService, ITagRepository tagRepository)
        {
            _userService = userService;
            _tagRepository = tagRepository;
        }

        public async Task<DataResponse<DeleteTagResponse>> Handle(DeleteTagRequest request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();

            if (userId == Guid.Empty)
                return ResponseFactory.Error<DeleteTagResponse>(ErrorType.UserIdNotFound);

            var result = await _tagRepository.DeleteTag(userId, request.Id, cancellationToken);
            return CreateResponse(result);
        }

        private static DataResponse<DeleteTagResponse> CreateResponse(int result)
        {
            if (result == -1)
                return ResponseFactory.Error<DeleteTagResponse>(ErrorType.FailedToDelete);
            return ResponseFactory.Success(GetResponse(result), SuccessType.DeletedItem);
        }

        private static DeleteTagResponse GetResponse(int deletedItems)
        {
            return new DeleteTagResponse
            {
                DeletedItems = deletedItems
            };
        }
    }
}
