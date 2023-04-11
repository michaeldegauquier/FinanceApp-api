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
        public async Task<IList<IncomeExpenseDto>> GetAllIncomesExpenses(Guid userId)
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

        /// <summary>
        /// Update one single income/expense for logged-in user
        /// </summary>
        /// <param name="updateIncomeExpense"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Amount updated</returns>
        public async Task<int> UpdateIncomeExpense(UpdateIncomeExpenseDto updateIncomeExpense, CancellationToken cancellationToken)
        {
            var incomeExpense = await _context.IncomesExpenses
                .Include(x => x.Tags)
                .Where(x => x.UserId == updateIncomeExpense.UserId)
                .FirstOrDefaultAsync(x => x.Id == updateIncomeExpense.Id);

            if (incomeExpense == null)
                return -1;

            var tagsToRemove = await _context.IncomeExpenseTags
                .Where(x => x.IncomeExpenseId == updateIncomeExpense.Id && !updateIncomeExpense.Tags.Contains(x.TagId))
                .ToListAsync();

            foreach (var tagToRemove in tagsToRemove)
                _context.IncomeExpenseTags.Remove(tagToRemove);

            var newTags = await _context.Tags
                .Where(x => x.UserId == updateIncomeExpense.UserId && updateIncomeExpense.Tags.Contains(x.Id))
                .ToListAsync();

            incomeExpense.DateCreated = updateIncomeExpense.DateCreated;
            incomeExpense.Amount = updateIncomeExpense.Amount;
            incomeExpense.Notes = updateIncomeExpense.Notes;
            incomeExpense.Tags = newTags;

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result;
        }

        /// <summary>
        /// Delete one single income/expense for logged-in user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Amount deleted</returns>
        public async Task<int> DeleteIncomeExpense(Guid userId, long id, CancellationToken cancellationToken)
        {
            var incomeExpense = await _context.IncomesExpenses
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (incomeExpense == null)
                return -1;

            var tagsToRemove = await _context.IncomeExpenseTags
                .Where(x => x.IncomeExpenseId == id)
                .ToListAsync();

            foreach (var tagToRemove in tagsToRemove)
                _context.IncomeExpenseTags.Remove(tagToRemove);

            _context.IncomesExpenses.Remove(incomeExpense);

            var result = await _context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
