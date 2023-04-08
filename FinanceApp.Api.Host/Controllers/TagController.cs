using FinanceApp.Api.Application.Handlers.TagHandlers.CreateTagHandler;
using FinanceApp.Api.Application.Handlers.TagHandlers.DeleteTagHandler;
using FinanceApp.Api.Application.Handlers.TagHandlers.GetAllTagsHandler;
using FinanceApp.Api.Application.Handlers.TagHandlers.GetTagByIdHandler;
using FinanceApp.Api.Application.Handlers.TagHandlers.UpdateTagHandler;
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

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(long id)
        {
            var result = await _mediator.Send(new GetTagByIdRequest
            {
                Id = id,
            });

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            else if (result?.Error?.ErrorType == ErrorType.ItemNotFound)
                return NotFound(result);
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTag(CreateTagRequest request)
        {
            var result = await _mediator.Send(request);

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(long id, UpdateTagRequest request)
        {
            request.Id = id;
            var result = await _mediator.Send(request);

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            else if (result?.Error?.ErrorType == ErrorType.FailedToUpdate)
                return UnprocessableEntity(result);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(long id)
        {
            var result = await _mediator.Send(new DeleteTagRequest 
            { 
                Id = id 
            });

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            else if (result?.Error?.ErrorType == ErrorType.FailedToDelete)
                return UnprocessableEntity(result);
            return Ok(result);
        }
    }
}
