using FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto;

namespace FinanceApp.Api.Application.Interfaces.Repositories
{
    public interface IIncomeExpenseRepository
    {
        Task<IList<IncomeExpenseDto>> GetAllIncomesExpenses(Guid userId);
        Task<IncomeExpenseDto?> GetIncomeExpenseById(Guid userId, long id);
        Task<long> CreateIncomeExpense(CreateIncomeExpenseDto createIncomeExpense, CancellationToken cancellationToken);
        Task<int> UpdateIncomeExpense(UpdateIncomeExpenseDto updateIncomeExpense, CancellationToken cancellationToken);
        Task<int> DeleteIncomeExpense(Guid userId, long id, CancellationToken cancellationToken);
    }
}
