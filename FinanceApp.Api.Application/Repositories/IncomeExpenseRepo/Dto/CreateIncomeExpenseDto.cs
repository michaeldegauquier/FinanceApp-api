﻿namespace FinanceApp.Api.Application.Repositories.IncomeExpenseRepo.Dto
{
    public class CreateIncomeExpenseDto
    {
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public double Amount { get; set; }
        public bool IsIncome => Amount >= 0;
        public string Notes { get; set; } = "";
        public IList<long> Tags { get; set; } = new List<long>();
    }
}
