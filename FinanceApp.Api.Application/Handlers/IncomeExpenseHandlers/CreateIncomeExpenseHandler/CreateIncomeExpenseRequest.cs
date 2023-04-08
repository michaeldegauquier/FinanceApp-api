using FinanceApp.Shared.Core.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace FinanceApp.Api.Application.Handlers.IncomeExpenseHandlers.CreateIncomeExpenseHandler
{
    public class CreateIncomeExpenseRequest : IRequest<DataResponse<CreateIncomeExpenseResponse>>
    {
        public DateTime DateCreated { get; set; }
        public double Amount { get; set; }
        [JsonIgnore]
        public bool IsIncome => Amount >= 0;
        public string Notes { get; set; } = "";
        public IEnumerable<long> Tags { get; set; } = new List<long>();
    }
}
