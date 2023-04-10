using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.CreateIncomeExpenseHandler
{
    public class CreateIncomeExpenseRequestHandler : IRequestHandler<CreateIncomeExpenseRequest, DataResponse<CreateIncomeExpenseResponse>>
    {
        private readonly IUserService _userService;
        private readonly IIncomeExpenseRepository _incomeExpenseRepository;

        public CreateIncomeExpenseRequestHandler(IUserService userService, IIncomeExpenseRepository incomeExpenseRepository)
        {
            _userService = userService;
            _incomeExpenseRepository = incomeExpenseRepository;
        }

        public async Task<DataResponse<CreateIncomeExpenseResponse>> Handle(CreateIncomeExpenseRequest request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();

            if (userId == Guid.Empty)
                return ResponseFactory.Error<CreateIncomeExpenseResponse>(ErrorType.UserIdNotFound);

            var result = await _incomeExpenseRepository.CreateIncomeExpense(new CreateIncomeExpenseDto
            {
                UserId = userId,
                DateCreated = request.DateCreated,
                Amount = request.Amount,
                Notes = request.Notes.Trim(),
                Tags = request.Tags,
            }, cancellationToken);

            return ResponseFactory.Success(GetResponse(result), SuccessType.CreatedItem);
        }

        private static CreateIncomeExpenseResponse GetResponse(long id)
        {
            return new CreateIncomeExpenseResponse
            {
                Id = id
            };
        }
    }
}
