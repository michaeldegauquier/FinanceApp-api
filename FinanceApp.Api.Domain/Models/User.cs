using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Api.Domain.Models
{
    public class User
    {
        [Key]
        public string? Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
