using System.Net;

namespace FinanceApp.Shared.Core.Responses
{
    public class DataResponse<T>
    {
        public T? Data { get; set; } = default!;
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;
        public string Message { get; set; } = "";
        public Error? Error { get; set; }

        public DataResponse() {}

        public DataResponse(T? data)
        {
            Data = data;
        }

        public DataResponse(HttpStatusCode status, string message, Error? error)
        {
            Status = status;
            Message = message;
            Error = error;
        }
    }
}
