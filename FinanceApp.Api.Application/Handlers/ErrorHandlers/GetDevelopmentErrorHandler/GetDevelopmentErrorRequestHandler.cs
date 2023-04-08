using MediatR;
using System.Net;

namespace FinanceApp.Api.Application.Handlers.ErrorHandlers.GetDevelopmentErrorHandler
{
    public class GetDevelopmentErrorRequestHandler : IRequestHandler<GetDevelopmentErrorRequest, GetDevelopmentErrorResponse>
    {
        public Task<GetDevelopmentErrorResponse> Handle(GetDevelopmentErrorRequest request, CancellationToken cancellationToken)
        {
            var exception = request.Context?.Error;

            if (exception is ArgumentException)
            {
                return Task.FromResult(GetResponse(HttpStatusCode.BadRequest, exception));
            }
            return Task.FromResult(GetResponse(HttpStatusCode.InternalServerError, exception));
        }

        private static GetDevelopmentErrorResponse GetResponse(HttpStatusCode statusCode, Exception? exception)
        {
            return new GetDevelopmentErrorResponse
            {
                Status = statusCode,
                Type = exception?.GetType()?.Name ?? "",
                Message = exception?.Message ?? "",
                StackTrace = exception?.ToString() ?? ""
            };
        }
    }
}
