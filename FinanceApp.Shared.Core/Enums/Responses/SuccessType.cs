using FinanceApp.Shared.Core.Attributes;

namespace FinanceApp.Shared.Core.Enums.Responses
{
    public enum SuccessType
    {
        [StatusCode(200), Message("User is successfully logged in!")]
        LoggedIn,
        [StatusCode(201) ,Message("User is successfully registered!")]
        Registered,
    }
}
