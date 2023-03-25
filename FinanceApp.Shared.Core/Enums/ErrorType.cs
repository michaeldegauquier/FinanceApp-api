using System.ComponentModel;

namespace FinanceApp.Shared.Core.Enums
{
    public enum ErrorType
    {
        [Description("No error.")]
        None,
        [Description("User already exists!")]
        UserAlreadyExists,
        [Description("Unable to create user, try again later.")]
        UnableToCreateUser,
    }
}
