namespace FinanceApp.Api.Application.Common.Dto
{
    public class IncomeExpenseDto
    {
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public double Amount { get; set; }
        public bool IsIncome => Amount >= 0;
        public string Notes { get; set; } = "";
        public IEnumerable<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}
