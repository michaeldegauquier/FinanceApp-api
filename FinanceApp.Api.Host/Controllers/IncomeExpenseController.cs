using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Api.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeExpenseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IncomeExpenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetUserId()
        {
            var user = getUserId();
            if (user == null)
                return BadRequest("no userid");
            return Ok(user);
        }

        // TODO create global method (just a test for now)
        private string? getUserId()
        {
            return HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
