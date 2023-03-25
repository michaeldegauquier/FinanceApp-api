using FinanceApp.Shared.Core.Enums;

namespace FinanceApp.Shared.Core.Responses
{
    public class DataResponse<T>
    {
        public T? Data { get; set; } = default!;
        public Error Error { get; set; } = new Error();
    }

    public class Error
    {
        public ErrorType ErrorType { get; set; } = ErrorType.None;
        public string Value { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
