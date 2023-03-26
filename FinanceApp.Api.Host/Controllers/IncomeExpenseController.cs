using MediatR;
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
    }
}
