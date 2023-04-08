namespace FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto
{
    public class CreateIncomeExpenseDto
    {
        public DateTime DateCreated { get; set; }
        public double Amount { get; set; }
        public bool IsIncome => Amount >= 0;
        public string Notes { get; set; } = "";
        public IEnumerable<long> Tags { get; set; } = new List<long>();
    }
}
