using FinanceApp.Api.Domain.Models;
using FinanceApp.Shared.Core.Enums;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FinanceApp.Api.Application.Handlers.Authentication.Register
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, DataResponse<RegisterResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterRequestHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DataResponse<RegisterResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (await UserExists(request.Email))
                return ResponseFactory.Error<RegisterResponse>(ErrorType.UserAlreadyExists);

            var user = await CreateNewUser(request);

            if (user.Succeeded)
                return ResponseFactory.Create(new RegisterResponse());
            return ResponseFactory.Error<RegisterResponse>(ErrorType.UnableToCreateUser);
        }

        private async Task<bool> UserExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        private async Task<IdentityResult> CreateNewUser(RegisterRequest request)
        {
            var user = GetUserToCreate(request);
            var result = await _userManager.CreateAsync(user, request.Password);
            return result;
        }

        private static ApplicationUser GetUserToCreate(RegisterRequest request)
        {
            return new ApplicationUser()
            {
                Email = request.Email.ToLower().Trim(),
                UserName = request.Email.ToLower().Trim(),
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim()
            };
        }
    }
}
