using FinanceApp.Api.Application.Interfaces;
using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories
{
    public class IncomeExpenseRepository : IIncomeExpenseRepository
    {
        private readonly IApplicationDbContext _context;

        public IncomeExpenseRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<IncomeExpense>> GetAllIncomesExpenses(string userId)
        {
            return await _context.IncomesExpenses
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
        }
    }
}
