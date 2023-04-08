using FinanceApp.Api.Application.Handlers.ErrorHandlers.GetDevelopmentErrorHandler;
using FinanceApp.Shared.Core.Factories;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Api.Host.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ErrorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returns a error response with stacktrace for development pruposes only
        /// </summary>
        /// <returns>Error response</returns>
        [Route("/error-development")]
        public async Task<IActionResult> ErrorDevelopment()
        {
            var request = new GetDevelopmentErrorRequest
            {
                Context = HttpContext.Features.Get<IExceptionHandlerFeature>()
            };

            var result = await _mediator.Send(request);
            return StatusCode((int)result.Status, result);
        }

        /// <summary>
        /// Returns error response understandable for end users
        /// </summary>
        /// <returns>Error response</returns>
        [Route("error")]
        public IActionResult Error()
        {
            var response = ResponseFactory.Error<string>(ErrorType.InternalServerError);
            return StatusCode(500, response);
        }
    }
}
