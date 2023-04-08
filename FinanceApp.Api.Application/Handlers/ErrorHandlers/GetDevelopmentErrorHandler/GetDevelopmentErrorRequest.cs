using MediatR;
using Microsoft.AspNetCore.Diagnostics;

namespace FinanceApp.Api.Application.Handlers.ErrorHandlers.GetDevelopmentErrorHandler
{
    public class GetDevelopmentErrorRequest : IRequest<GetDevelopmentErrorResponse>
    {
        public IExceptionHandlerFeature? Context { get; set; }
    }
}
