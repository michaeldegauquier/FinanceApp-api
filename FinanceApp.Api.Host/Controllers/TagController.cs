using FinanceApp.Api.Application.Handlers.Tag.GetAllTags;
using FinanceApp.Shared.Core.Responses.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Api.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var result = await _mediator.Send(new GetAllTagsRequest());

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            return Ok(result);
        }
    }
}
