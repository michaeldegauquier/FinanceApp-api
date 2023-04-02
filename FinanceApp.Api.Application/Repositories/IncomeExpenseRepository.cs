using AutoMapper;
using FinanceApp.Api.Application.Common.Dto;
using FinanceApp.Api.Application.Interfaces;
using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Application.Repositories
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

            return GetResult(result);
        }

        private List<IncomeExpenseDto> GetResult(List<IncomeExpense> incomesExpenses)
        {
            var result = _mapper.Map<List<IncomeExpenseDto>>(incomesExpenses);

            if (result == null)
                return new List<IncomeExpenseDto>();
            return result;
        }
    }
}
