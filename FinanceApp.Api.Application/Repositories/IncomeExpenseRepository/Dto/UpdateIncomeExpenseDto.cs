namespace FinanceApp.Api.Application.Repositories.IncomeExpenseRepository.Dto
{
    public class UpdateIncomeExpenseDto
    {
        public Guid UserId { get; set; }
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public double Amount { get; set; }
        public bool IsIncome => Amount >= 0;
        public string Notes { get; set; } = "";
        public IEnumerable<long> Tags { get; set; } = new List<long>();
    }
}
