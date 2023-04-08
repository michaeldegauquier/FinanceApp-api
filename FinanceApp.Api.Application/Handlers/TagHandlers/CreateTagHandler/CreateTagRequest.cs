using FinanceApp.Shared.Core.Responses;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.CreateTagHandler
{
    public class CreateTagRequest : IRequest<DataResponse<CreateTagResponse>>
    {
        public string Name { get; set; } = "";
    }
}
