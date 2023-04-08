using FinanceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<IncomeExpense> IncomesExpenses { get; set; }
        DbSet<Tag> Tags { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
