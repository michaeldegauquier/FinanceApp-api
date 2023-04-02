using FinanceApp.Api.Application.Common.Dto;

namespace FinanceApp.Api.Application.Interfaces.Repositories
{
    public interface IIncomeExpenseRepository
    {
        Task<IEnumerable<IncomeExpenseDto>> GetAllIncomesExpenses(Guid userId);
    }
}
