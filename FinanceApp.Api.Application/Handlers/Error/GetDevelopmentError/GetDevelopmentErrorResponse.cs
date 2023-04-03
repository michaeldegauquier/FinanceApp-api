using System.Net;

namespace FinanceApp.Api.Application.Handlers.Error.GetDevelopmentError
{
    public class GetDevelopmentErrorResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Type { get; set; } = "";
        public string Message { get; set; } = "";
        public string StackTrace { get; set; } = "";
    }
}
