using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo.Dto;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.UpdateIncomeExpenseHandler
{
    public class UpdateIncomeExpenseRequestHandler : IRequestHandler<UpdateIncomeExpenseRequest, DataResponse<UpdateIncomeExpenseResponse>>
    {
        private readonly IUserService _userService;
        private readonly IIncomeExpenseRepository _incomeExpenseRepository;

        public UpdateIncomeExpenseRequestHandler(IUserService userService, IIncomeExpenseRepository incomeExpenseRepository)
        {
            _userService = userService;
            _incomeExpenseRepository = incomeExpenseRepository;
        }

        public async Task<DataResponse<UpdateIncomeExpenseResponse>> Handle(UpdateIncomeExpenseRequest request, CancellationToken cancellationToken)
        {
            var userId = _userService.GetUserId();

            if (userId == Guid.Empty)
                return ResponseFactory.Error<UpdateIncomeExpenseResponse>(ErrorType.UserIdNotFound);

            var result = await _incomeExpenseRepository.UpdateIncomeExpense(new UpdateIncomeExpenseDto
            {
                UserId = userId,
                Id = request.Id,    
                DateCreated = request.DateCreated,
                Amount = request.Amount,
                Notes = request.Notes.Trim(),
                Tags = request.Tags,
            }, cancellationToken);
            return CreateResponse(result);
        }

        private static DataResponse<UpdateIncomeExpenseResponse> CreateResponse(int result)
        {
            if (result == -1)
                return ResponseFactory.Error<UpdateIncomeExpenseResponse>(ErrorType.FailedToUpdate);
            return ResponseFactory.Success(GetResponse(result), SuccessType.UpdatedItem);
        }

        private static UpdateIncomeExpenseResponse GetResponse(int updatedItems)
        {
            return new UpdateIncomeExpenseResponse
            {
                UpdatedItems = updatedItems
            };
        }
    }
}
