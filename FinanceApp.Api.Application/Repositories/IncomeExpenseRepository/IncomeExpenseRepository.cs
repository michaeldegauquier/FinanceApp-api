using AutoMapper;
using FinanceApp.Api.Application.Interfaces;
using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto;
using FinanceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories.IncomeExpenseRepository
{
    public class IncomeExpenseRepository : IIncomeExpenseRepository
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public IncomeExpenseRepository(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<IncomeExpenseDto>> GetAllIncomesExpenses(Guid userId)
        {
            var result = await _context.IncomesExpenses
                .Include(x => x.Tags)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();

            if (result == null)
                return new List<IncomeExpenseDto>();

            return MapAllResults(result);
        }

        public async Task<IncomeExpenseDto> GetIncomeExpenseById(Guid userId, long id)
        {
            var result = await _context.IncomesExpenses
                .Include(x => x.Tags)
                .Where(x => x.UserId == userId && x.Id == id)
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefaultAsync();

            if (result == null)
                return new IncomeExpenseDto();

            return MapResult(result);
        }

        private List<IncomeExpenseDto> MapAllResults(List<IncomeExpense> incomesExpenses)
        {
            var result = _mapper.Map<List<IncomeExpenseDto>>(incomesExpenses);

            if (result == null)
                return new List<IncomeExpenseDto>();
            return result;
        }

        private IncomeExpenseDto MapResult(IncomeExpense incomeExpense)
        {
            var result = _mapper.Map<IncomeExpenseDto>(incomeExpense);

            if (result == null)
                return new IncomeExpenseDto();
            return result;
        }
    }
}
