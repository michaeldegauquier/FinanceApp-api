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
        [StatusCode(HttpStatusCode.OK), Message("Successfully returned data!")]
        DataFound,
        [StatusCode(HttpStatusCode.OK), Message("Successfully created item!")]
        CreatedItem,
        [StatusCode(HttpStatusCode.OK), Message("Successfully updated item!")]
        UpdatedItem,
        [StatusCode(HttpStatusCode.OK), Message("Successfully deleted item!")]
        DeletedItem,
    }
}
