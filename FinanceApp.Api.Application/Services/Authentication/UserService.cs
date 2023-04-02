using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FinanceApp.Api.Application.Services.Authentication
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Guid.Empty;

            if (Guid.TryParse(userId, out Guid guidUserId))
                return guidUserId;

            return Guid.Empty;
        }
    }
}
