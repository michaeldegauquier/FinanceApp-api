namespace FinanceApp.Shared.Core.Responses
{
    public class DataResponse<T>
    {
        public T? Data { get; set; } = default!;
        public int Status { get; set; } = 0;
        public string Message { get; set; } = "";
        public Error? Error { get; set; }
    }
}
