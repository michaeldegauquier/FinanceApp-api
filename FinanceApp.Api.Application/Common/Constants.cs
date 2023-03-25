namespace FinanceApp.Api.Application.Common
{
    public static class Constants
    {
        public static class Jwt
        {
            public static readonly string Secret = "Jwt:Secret";
            public static readonly string ExpiryInMinutes = "Jwt:ExpiryInMinutes";
            public static readonly string ValidIssuer = "Jwt:ValidIssuer";
            public static readonly string ValidAudience = "Jwt:ValidAudience";
        }
    }
}
