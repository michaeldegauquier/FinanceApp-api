using FinanceApp.Shared.Core.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.UpdateIncomeExpenseHandler
{
    public class UpdateIncomeExpenseRequest : IRequest<DataResponse<UpdateIncomeExpenseResponse>>
    {
        [JsonIgnore]
        public long Id { get; set; }
        public DateTime DateCreated { get; set; }
        public double Amount { get; set; }
        [JsonIgnore]
        public bool IsIncome => Amount >= 0;
        public string Notes { get; set; } = "";
        public IList<long> Tags { get; set; } = new List<long>();
    }
}
