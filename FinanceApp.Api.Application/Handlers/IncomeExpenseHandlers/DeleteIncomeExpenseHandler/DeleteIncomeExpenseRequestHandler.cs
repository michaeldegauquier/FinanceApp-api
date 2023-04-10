using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.DeleteIncomeExpenseHandler
{
    public class DeleteIncomeExpenseRequestHandler : IRequestHandler<DeleteIncomeExpenseRequest, DataResponse<DeleteIncomeExpenseResponse>>
    {
        private readonly IUserService _userService;
        private readonly IIncomeExpenseRepository _incomeExpenseRepository;

        public DeleteIncomeExpenseRequestHandler(IUserService userService, IIncomeExpenseRepository incomeExpenseRepository)
        {
            _userService = userService;
            _incomeExpenseRepository = incomeExpenseRepository;
        }

        public async Task<DataResponse<DeleteIncomeExpenseResponse>> Handle(DeleteIncomeExpenseRequest request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();

            if (userId == Guid.Empty)
                return ResponseFactory.Error<DeleteIncomeExpenseResponse>(ErrorType.UserIdNotFound);

            var result = await _incomeExpenseRepository.DeleteIncomeExpense(userId, request.Id, cancellationToken);
            return CreateResponse(result);
        }

        private static DataResponse<DeleteIncomeExpenseResponse> CreateResponse(int result)
        {
            if (result == -1)
                return ResponseFactory.Error<DeleteIncomeExpenseResponse>(ErrorType.FailedToDelete);
            return ResponseFactory.Success(GetResponse(result), SuccessType.DeletedItem);
        }

        private static DeleteIncomeExpenseResponse GetResponse(int deletedItems)
        {
            return new DeleteIncomeExpenseResponse
            {
                DeletedItems = deletedItems
            };
        }
    }
}
