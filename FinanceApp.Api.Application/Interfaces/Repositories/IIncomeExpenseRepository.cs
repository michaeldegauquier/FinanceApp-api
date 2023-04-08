﻿using FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto;

namespace FinanceApp.Api.Application.Interfaces.Repositories
{
    public interface IIncomeExpenseRepository
    {
        Task<IEnumerable<IncomeExpenseDto>> GetAllIncomesExpenses(Guid userId);
    }
}
