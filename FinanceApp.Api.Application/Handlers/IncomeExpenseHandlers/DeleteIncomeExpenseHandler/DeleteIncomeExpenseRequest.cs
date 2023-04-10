using FinanceApp.Shared.Core.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.DeleteIncomeExpenseHandler
{
    public class DeleteIncomeExpenseRequest : IRequest<DataResponse<DeleteIncomeExpenseResponse>>
    {
        [JsonIgnore]
        public long Id { get; set; }
    }
}
