using FinanceApp.Api.Application.Common.AutoMapper;
using FinanceApp.Api.Application.Interfaces;
using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto;
using FinanceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.IncomeExpenseRepository
{
    public class IncomeExpenseRepository : IIncomeExpenseRepository
    {
        private readonly IApplicationDbContext _context;

        public IncomeExpenseRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all incomes/expenses for logged-in user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of incomes/expenses</returns>
        public async Task<IEnumerable<IncomeExpenseDto>> GetAllIncomesExpenses(Guid userId)
        {
            var result = await _context.IncomesExpenses
                .Include(x => x.Tags)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();

            return Mapper.MapList<IncomeExpense?, IncomeExpenseDto>(result);
        }

        /// <summary>
        /// Get one single income/expense for logged-in user by incomeExpenseId
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns>Income/expense</returns>
        public async Task<IncomeExpenseDto?> GetIncomeExpenseById(Guid userId, long id)
        {
            var result = await _context.IncomesExpenses
                .Include(x => x.Tags)
                .Where(x => x.UserId == userId && x.Id == id)
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefaultAsync();

            return Mapper.Map<IncomeExpense?, IncomeExpenseDto?>(result);
        }

        /// <summary>
        /// Create one single income/expense for logged-in user
        /// </summary>
        /// <param name="createIncomeExpense"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>IncomeExpenseId</returns>
        public async Task<long> CreateIncomeExpense(CreateIncomeExpenseDto createIncomeExpense, CancellationToken cancellationToken)
        {
            var tags = await _context.Tags
                .Where(x => createIncomeExpense.Tags.Contains(x.Id))
                .ToListAsync();

            var incomeExpenseToCreate = new IncomeExpense
            {
                UserId = createIncomeExpense.UserId,
                DateCreated = createIncomeExpense.DateCreated,
                Amount = createIncomeExpense.Amount,
                Notes = createIncomeExpense.Notes,
                Tags = tags
            };

            await _context.IncomesExpenses.AddAsync(incomeExpenseToCreate);
            await _context.SaveChangesAsync(cancellationToken);

            return incomeExpenseToCreate.Id;
        }
    }
}
