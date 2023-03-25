namespace FinanceApp.Api.Application.Handlers.Authentication.Login
{
    public class LoginResponse
    {
        public string Token { get; set; } = "";
        public DateTime Expiration { get; set; }
    }
}
