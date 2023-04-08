using FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetAllIncomesExpensesHandler
{
    public class GetAllIncomesExpensesResponse
    {
        public IEnumerable<IncomeExpenseDto> IncomesExpenses { get; set; } = new List<IncomeExpenseDto>();
    }
}
