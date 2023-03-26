using MediatR;
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
    }
}
