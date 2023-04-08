using System.Net;

namespace FinanceApp.Api.Application.Handlers.ErrorHandlers.GetDevelopmentErrorHandler
{
    public class GetDevelopmentErrorResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Type { get; set; } = "";
        public string Message { get; set; } = "";
        public string StackTrace { get; set; } = "";
    }
}
