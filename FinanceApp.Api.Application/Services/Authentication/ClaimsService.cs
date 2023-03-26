using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinanceApp.Api.Application.Services.Authentication
{
    public class ClaimsService : IClaimsService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ClaimsService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<Claim>> GetAuthClaimsAsync(ApplicationUser user)
        {
            var authClaims = GetAuthClaims(user);
            await AddUserRoles(authClaims, user);
            return authClaims;
        }

        private static List<Claim> GetAuthClaims(ApplicationUser user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
        }

        private async Task AddUserRoles(List<Claim> authClaims, ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
        }
    }
}
