using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Api.Domain.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
    }
}
