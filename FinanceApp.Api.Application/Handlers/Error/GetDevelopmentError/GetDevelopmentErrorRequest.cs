using MediatR;
using Microsoft.AspNetCore.Diagnostics;

namespace FinanceApp.Api.Application.Handlers.Error.GetDevelopmentError
{
    public class GetDevelopmentErrorRequest : IRequest<GetDevelopmentErrorResponse>
    {
        public IExceptionHandlerFeature? Context { get; set; }
    }
}
