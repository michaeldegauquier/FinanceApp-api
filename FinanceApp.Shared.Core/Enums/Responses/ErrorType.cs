using FinanceApp.Shared.Core.Attributes;

namespace FinanceApp.Shared.Core.Enums.Responses
{
    public enum ErrorType
    {
        [StatusCode(401), Message("Email or password is incorrect!")]
        WrongEmailOrPassword,
        [StatusCode(409), Message("User already exists!")]
        UserAlreadyExists,
        [StatusCode(400), Message("Unable to create user, try again later.")]
        UnableToCreateUser,
    }
}
