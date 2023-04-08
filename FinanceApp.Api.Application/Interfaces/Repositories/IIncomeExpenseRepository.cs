using FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto;

namespace FinanceApp.Api.Application.Interfaces.Repositories
{
    public interface IIncomeExpenseRepository
    {
        Task<IEnumerable<IncomeExpenseDto>> GetAllIncomesExpenses(Guid userId);
        Task<IncomeExpenseDto?> GetIncomeExpenseById(Guid userId, long id);
        Task<long> CreateIncomeExpense(CreateIncomeExpenseDto createIncomeExpense, CancellationToken cancellationToken);
    }
}
