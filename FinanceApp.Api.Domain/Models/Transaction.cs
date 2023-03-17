using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Api.Domain.Models
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime DateCreated { get; set; }
        public string? Description { get; set; }
        [Required]
        public double? Amount { get; set; }
        [Required]
        public string? UserId { get; set; }
        public User? User { get; set; }
        public long? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
