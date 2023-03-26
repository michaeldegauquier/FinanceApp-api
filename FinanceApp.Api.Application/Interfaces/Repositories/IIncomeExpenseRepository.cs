using FinanceApp.Api.Domain.Models;

namespace FinanceApp.Api.Application.Interfaces.Repositories
{
    public interface IIncomeExpenseRepository
    {
        Task<ICollection<IncomeExpense>> GetAllIncomesExpenses(string userId);
    }
}
