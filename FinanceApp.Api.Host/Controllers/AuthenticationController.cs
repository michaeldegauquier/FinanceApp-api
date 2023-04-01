using FinanceApp.Api.Application.Handlers.Authentication.Login;
using FinanceApp.Api.Application.Handlers.Authentication.Register;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Api.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Login with a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Token</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _mediator.Send(request);

            if (result?.Error?.ErrorType == ErrorType.WrongEmailOrPassword)
                return Unauthorized(result);

            return Ok(result);
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _mediator.Send(request);

            if (result?.Error?.ErrorType == ErrorType.UserAlreadyExists)
                return Conflict(result);
            else if (result?.Error?.ErrorType == ErrorType.UnableToCreateUser) {
                return BadRequest(result);
            }
            return Created("Created", result);
        }
    }
}
