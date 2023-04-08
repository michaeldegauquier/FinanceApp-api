using FinanceApp.Shared.Core.Responses;
using MediatR;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.GetAllTagsHandler
{
    public class GetAllTagsRequest : IRequest<DataResponse<GetAllTagsResponse>>
    {
    }
}
