using System.Net;

namespace FinanceApp.Shared.Core.Attributes
{
    public class StatusCodeAttribute : Attribute
    {
        public HttpStatusCode StatusCode { get; private set; }

        public StatusCodeAttribute(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}