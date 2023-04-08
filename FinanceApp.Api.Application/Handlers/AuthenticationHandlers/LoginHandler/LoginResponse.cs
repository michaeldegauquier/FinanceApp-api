namespace FinanceApp.Api.Application.Handlers.AuthenticationHandlers.LoginHandler
{
    public class LoginResponse
    {
        public string Token { get; set; } = "";
        public DateTime Expiration { get; set; }
    }
}
