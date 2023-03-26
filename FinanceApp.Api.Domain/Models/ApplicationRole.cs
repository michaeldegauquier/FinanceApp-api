using Microsoft.AspNetCore.Identity;

namespace FinanceApp.Api.Domain.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string name)
        {
            Name = name;
        }
    }
}
