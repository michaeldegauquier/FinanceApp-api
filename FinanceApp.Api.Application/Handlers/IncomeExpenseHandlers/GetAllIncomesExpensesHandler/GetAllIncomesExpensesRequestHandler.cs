using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetAllIncomesExpensesHandler
{
    public class GetAllIncomesExpensesRequestHandler : IRequestHandler<GetAllIncomesExpensesRequest, DataResponse<GetAllIncomesExpensesResponse>>
    {
        private readonly IUserService _userService;
        private readonly IIncomeExpenseRepository _incomeExpenseRepository;

        public GetAllIncomesExpensesRequestHandler(IUserService userService, IIncomeExpenseRepository incomeExpenseRepository)
        {
            _userService = userService;
            _incomeExpenseRepository = incomeExpenseRepository;
        }

        public async Task<DataResponse<GetAllIncomesExpensesResponse>> Handle(GetAllIncomesExpensesRequest request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();

            if (userId == Guid.Empty)
                return ResponseFactory.Error<GetAllIncomesExpensesResponse>(ErrorType.UserIdNotFound);

            var incomesExpenses = await _incomeExpenseRepository.GetAllIncomesExpenses(userId);
            return ResponseFactory.Success(GetResponse(incomesExpenses), SuccessType.DataFound);
        }

        private static GetAllIncomesExpensesResponse GetResponse(IEnumerable<IncomeExpenseDto> incomesExpenses)
        {
            return new GetAllIncomesExpensesResponse
            {
                IncomesExpenses = incomesExpenses
            };
        }
    }
}
