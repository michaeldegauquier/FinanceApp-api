using FinanceApp.Api.Application.Common.Dto;

namespace FinanceApp.Api.Application.Handlers.IncomeExpense.GetAllIncomesExpenses
{
    public class GetAllIncomesExpensesResponse
    {
        public IEnumerable<IncomeExpenseDto> IncomesExpenses { get; set; } = new List<IncomeExpenseDto>();
    }
}
