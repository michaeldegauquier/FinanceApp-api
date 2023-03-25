using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace FinanceApp.Api.Application.Handlers.Authentication.Login
{
    public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClaimsService _claimsService;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginRequestHandler(
            UserManager<ApplicationUser> userManager, 
            IClaimsService claimsService,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _claimsService = claimsService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (await IsValidUser(user, request.Password))
            {
                var authClaims = await _claimsService.GetAuthClaimsAsync(user);
                var token = _jwtTokenService.GetJwtToken(authClaims);

                return new LoginResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo
                };
            }
            return new LoginResponse();
        }

        private async Task<bool> IsValidUser(ApplicationUser user, string password)
        {
            return user != null && await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
