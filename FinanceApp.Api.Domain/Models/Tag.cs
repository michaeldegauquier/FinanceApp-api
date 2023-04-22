using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Api.Domain.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public long Id { get; set; }
        [Required, MinLength(1), MaxLength(20)]
        public string Name { get; set; } = "";

        [Required]
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<IncomeExpense> IncomesExpenses { get; set; } = new List<IncomeExpense>();
        public ICollection<IncomeExpenseTag> IncomeExpenseTags { get; set; } = new List<IncomeExpenseTag>();
    }
}
