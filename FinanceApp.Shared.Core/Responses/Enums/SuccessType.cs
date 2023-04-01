using FinanceApp.Shared.Core.Attributes;
using System.Net;

namespace FinanceApp.Shared.Core.Responses.Enums
{
    public enum SuccessType
    {
        [StatusCode(HttpStatusCode.OK), Message("User is successfully logged in!")]
        LoggedIn,
        [StatusCode(HttpStatusCode.Created), Message("User is successfully registered!")]
        Registered,
    }
}
