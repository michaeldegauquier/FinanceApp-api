using FinanceApp.Shared.Core.Attributes;
using System.Net;

namespace FinanceApp.Shared.Core.Responses.Enums
{
    public enum ErrorType
    {
        [StatusCode(HttpStatusCode.InternalServerError), Message("System Error! Please try again later.")]
        InternalServerError,
        [StatusCode(HttpStatusCode.Unauthorized), Message("Email or password is incorrect!")]
        WrongEmailOrPassword,
        [StatusCode(HttpStatusCode.Conflict), Message("User already exists!")]
        UserAlreadyExists,
        [StatusCode(HttpStatusCode.BadRequest), Message("Unable to create user, try again later.")]
        UnableToCreateUser,
        [StatusCode(HttpStatusCode.Unauthorized), Message("No userId found!")]
        UserIdNotFound,
        [StatusCode(HttpStatusCode.NotFound), Message("Item not found!")]
        ItemNotFound,
        [StatusCode(HttpStatusCode.UnprocessableEntity), Message("Failed to create item.")]
        FailedToCreate,
        [StatusCode(HttpStatusCode.UnprocessableEntity), Message("Failed to update item.")]
        FailedToUpdate,
        [StatusCode(HttpStatusCode.UnprocessableEntity), Message("Failed to delete item.")]
        FailedToDelete,
        [StatusCode(HttpStatusCode.Conflict), Message("Failed to create item because it already exists.")]
        DuplicateCreateItem,
        [StatusCode(HttpStatusCode.Conflict), Message("Failed to update item because it already exists.")]
        DuplicateUpdateItem,
    }
}
