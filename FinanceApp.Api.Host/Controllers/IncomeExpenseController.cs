using FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.CreateIncomeExpenseHandler;
using FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetAllIncomesExpensesHandler;
using FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetIncomeExpenseByIdHandler;
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

        /// <summary>
        /// Get all incomes/expenses
        /// </summary>
        /// <returns>All incomes/expenses</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllIncomesExpenses()
        {
            var result = await _mediator.Send(new GetAllIncomesExpensesRequest());

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            return Ok(result);
        }

        /// <summary>
        /// Get income/expense by incomeExpenseId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Income/expense</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomeExpenseById(long id)
        {
            var result = await _mediator.Send(new GetIncomeExpenseByIdRequest
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
        /// Creates a new income/expense
        /// </summary>
        /// <param name="request"></param>
        /// <returns>IncomeExpenseId</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateIncomeExpenseRequest request)
        {
            var result = await _mediator.Send(request);

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            return Ok(result);
        }
    }
}
