using FinanceApp.Api.Application.Repositories.IncomeExpenseRepo.Dto;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetAllIncomesExpensesHandler
{
    public class GetAllIncomesExpensesResponse
    {
        public IList<IncomeExpenseDto> IncomesExpenses { get; set; } = new List<IncomeExpenseDto>();
    }
}
