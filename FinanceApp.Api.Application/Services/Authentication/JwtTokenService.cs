using FinanceApp.Api.Application.Common;
using FinanceApp.Api.Application.Interfaces.Services;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceApp.Api.Application.Services.Authentication
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfigurationService _configService;

        public JwtTokenService(IConfigurationService configService)
        {
            _configService = configService;
        }

        public JwtSecurityToken GetJwtToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configService.GetConfigVariable(Constants.Jwt.Secret)));
            var expiryInMinutes = Convert.ToInt32(_configService.GetConfigVariable(Constants.Jwt.ExpiryInMinutes));

            return new JwtSecurityToken(
                issuer: _configService.GetConfigVariable(Constants.Jwt.ValidIssuer),
                audience: _configService.GetConfigVariable(Constants.Jwt.ValidAudience),
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(expiryInMinutes),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
