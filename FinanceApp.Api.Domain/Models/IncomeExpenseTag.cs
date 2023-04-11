namespace FinanceApp.Api.Domain.Models
{
    public class IncomeExpenseTag
    {
        public long IncomeExpenseId { get; set; }
        public IncomeExpense IncomeExpense { get; set; } = new IncomeExpense();

        public long TagId { get; set; }
        public Tag Tag { get; set; } = new Tag();
    }
}
