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

        /// <summary>
        /// Get all tags for specific user
        /// </summary>
        /// <returns>All tags</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var result = await _mediator.Send(new GetAllTagsRequest());

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            return Ok(result);
        }

        /// <summary>
        /// Get tag by tagId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tag</returns>
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

        /// <summary>
        /// Creates a new tag
        /// </summary>
        /// <param name="request"></param>
        /// <returns>TagId</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagRequest request)
        {
            var result = await _mediator.Send(request);

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            else if (result?.Error?.ErrorType == ErrorType.DuplicateCreateItem)
                return Conflict(result);
            return Ok(result);
        }

        /// <summary>
        /// Updates a tag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Amount updated records</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(long id, [FromBody] UpdateTagRequest request)
        {
            request.Id = id;
            var result = await _mediator.Send(request);

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            else if (result?.Error?.ErrorType == ErrorType.DuplicateUpdateItem)
                return Conflict(result);
            else if (result?.Error?.ErrorType == ErrorType.FailedToUpdate)
                return UnprocessableEntity(result);
            return Ok(result);
        }

        /// <summary>
        /// Deletes a tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns>AMount deleted records</returns>
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
