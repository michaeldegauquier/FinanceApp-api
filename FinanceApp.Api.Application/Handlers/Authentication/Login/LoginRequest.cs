using MediatR;

namespace FinanceApp.Api.Application.Handlers.Authentication.Login
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
