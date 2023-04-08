using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetIncomeExpenseByIdHandler
{
    public class GetIncomeExpenseByIdRequestHandler : IRequestHandler<GetIncomeExpenseByIdRequest, DataResponse<GetIncomeExpenseByIdResponse>>
    {
        private readonly IUserService _userService;
        private readonly IIncomeExpenseRepository _incomeExpenseRepository;

        public GetIncomeExpenseByIdRequestHandler(IUserService userService, IIncomeExpenseRepository incomeExpenseRepository)
        {
            _userService = userService;
            _incomeExpenseRepository = incomeExpenseRepository;
        }

        public async Task<DataResponse<GetIncomeExpenseByIdResponse>> Handle(GetIncomeExpenseByIdRequest request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();

            if (userId == Guid.Empty)
                return ResponseFactory.Error<GetIncomeExpenseByIdResponse>(ErrorType.UserIdNotFound);

            var incomeExpense = await _incomeExpenseRepository.GetIncomeExpenseById(userId, request.Id);
            return CreateResponse(incomeExpense);
        }

        private static DataResponse<GetIncomeExpenseByIdResponse> CreateResponse(IncomeExpenseDto? incomeExpense)
        {
            if (incomeExpense == null)
                return ResponseFactory.Error<GetIncomeExpenseByIdResponse>(ErrorType.ItemNotFound);

            return ResponseFactory.Success(GetResponse(incomeExpense), SuccessType.DataFound);
        }

        private static GetIncomeExpenseByIdResponse GetResponse(IncomeExpenseDto? incomeExpense)
        {
            return new GetIncomeExpenseByIdResponse
            {
                IncomeExpense = incomeExpense
            };
        }
    }
}
