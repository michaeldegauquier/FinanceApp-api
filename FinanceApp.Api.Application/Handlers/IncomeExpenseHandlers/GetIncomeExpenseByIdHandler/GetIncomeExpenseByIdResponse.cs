using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo.Dto;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetIncomeExpenseByIdHandler
{
    public class GetIncomeExpenseByIdResponse
    {
        public IncomeExpenseDto? IncomeExpense { get; set; } = new IncomeExpenseDto();
    }
}
