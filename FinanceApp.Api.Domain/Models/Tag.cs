﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceApp.Api.Domain.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; } = "";

        [Required]
        public Guid UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public virtual IEnumerable<IncomeExpense> IncomesExpenses { get; set; } = new List<IncomeExpense>();
        public List<IncomeExpenseTag>? IncomeExpenseTags { get; set; }
    }
}
