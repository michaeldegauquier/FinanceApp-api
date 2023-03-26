using FinanceApp.Shared.Core.Responses;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.Authentication.Login
{
    public class LoginRequest : IRequest<DataResponse<LoginResponse>>
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
