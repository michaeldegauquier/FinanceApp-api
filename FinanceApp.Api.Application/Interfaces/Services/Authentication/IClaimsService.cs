using FinanceApp.Api.Domain.Models;
using System.Security.Claims;

namespace FinanceApp.Api.Application.Interfaces.Services.Authentication
{
    public interface IClaimsService
    {
        Task<List<Claim>> GetAuthClaimsAsync(ApplicationUser user);
    }
}
