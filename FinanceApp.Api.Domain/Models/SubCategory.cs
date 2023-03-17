using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Api.Domain.Models
{
    public class SubCategory
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
