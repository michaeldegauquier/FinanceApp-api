using FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.CreateIncomeExpenseHandler;
using FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.DeleteIncomeExpenseHandler;
using FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetAllIncomesExpensesHandler;
using FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetIncomeExpenseByIdHandler;
using FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.UpdateIncomeExpenseHandler;
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
        /// Create a new income/expense
        /// </summary>
        /// <param name="request"></param>
        /// <returns>IncomeExpenseId</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateIncomeExpense([FromBody] CreateIncomeExpenseRequest request)
        {
            var result = await _mediator.Send(request);

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            return Ok(result);
        }

        /// <summary>
        /// Update an income/expense
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>Amount updated records</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncomeExpense(long id, [FromBody] UpdateIncomeExpenseRequest request)
        {
            request.Id = id;
            var result = await _mediator.Send(request);

            if (result?.Error?.ErrorType == ErrorType.UserIdNotFound)
                return Unauthorized(result);
            else if (result?.Error?.ErrorType == ErrorType.FailedToUpdate)
                return UnprocessableEntity(result);
            return Ok(result);
        }

        /// <summary>
        /// Delete an income/expense
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Amount deleted records</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncomeExpense(long id)
        {
            var result = await _mediator.Send(new DeleteIncomeExpenseRequest
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
