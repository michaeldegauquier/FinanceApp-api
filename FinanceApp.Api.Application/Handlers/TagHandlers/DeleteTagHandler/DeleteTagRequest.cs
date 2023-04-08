using FinanceApp.Shared.Core.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.DeleteTagHandler
{
    public class DeleteTagRequest : IRequest<DataResponse<DeleteTagResponse>>
    {
        [JsonIgnore]
        public long Id { get; set; }
    }
}
