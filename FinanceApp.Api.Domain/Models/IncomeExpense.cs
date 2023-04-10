using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Api.Domain.Models
{
    [Table("IncomesExpenses")]
    public class IncomeExpense
    {
        [Key]
        public long Id { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public bool IsIncome => Amount >= 0;
        public string Notes { get; set; } = "";

        [Required]
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<IncomeExpenseTag>? IncomeExpenseTags { get; set; }
    }
}
