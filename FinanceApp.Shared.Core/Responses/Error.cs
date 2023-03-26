using FinanceApp.Shared.Core.Enums.Responses;

namespace FinanceApp.Shared.Core.Responses
{
    public class Error
    {
        public ErrorType ErrorType { get; set; }
        public string Code { get; set; } = "";
    }
}
