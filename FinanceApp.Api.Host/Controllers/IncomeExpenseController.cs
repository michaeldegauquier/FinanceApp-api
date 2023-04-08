using FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetAllIncomesExpensesHandler;
using FinanceApp.Shared.Core.Responses.Enums;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllIncomesExpenses()
        {
            var result = await _mediator.Send(new GetAllIncomesExpensesRequest());

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            return Ok(result);
        }
    }
}
