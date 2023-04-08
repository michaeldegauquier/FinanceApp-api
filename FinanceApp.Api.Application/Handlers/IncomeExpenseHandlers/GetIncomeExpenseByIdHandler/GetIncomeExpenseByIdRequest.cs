using FinanceApp.Shared.Core.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.GetIncomeExpenseByIdHandler
{
    public class GetIncomeExpenseByIdRequest : IRequest<DataResponse<GetIncomeExpenseByIdResponse>>
    {
        [JsonIgnore]
        public long Id { get; set; }
    }
}
