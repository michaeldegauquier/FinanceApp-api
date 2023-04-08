using FinanceApp.Shared.Core.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.GetTagByIdHandler
{
    public class GetTagByIdRequest : IRequest<DataResponse<GetTagByIdResponse>>
    {
        [JsonIgnore]
        public long Id { get; set; }
    }
}
