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
    }
}
