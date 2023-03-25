using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinanceApp.Api.Application.Interfaces.Services.Authentication
{
    public interface IJwtTokenService
    {
        JwtSecurityToken GetJwtToken(List<Claim> authClaims);
    }
}
