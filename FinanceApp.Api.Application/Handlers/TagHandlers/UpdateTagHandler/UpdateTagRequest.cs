using FinanceApp.Shared.Core.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace FinanceApp.Api.Application.Handlers.TagHandlers.UpdateTagHandler
{
    public class UpdateTagRequest : IRequest<DataResponse<UpdateTagResponse>>
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; } = "";
    }
}
