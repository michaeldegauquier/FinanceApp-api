using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Api.Domain.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public long? CategoryId { get; set; }
        public virtual SubCategory? SubCategory { get; set; }
    }
}
