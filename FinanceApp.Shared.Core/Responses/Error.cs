using FinanceApp.Shared.Core.Responses.Enums;

namespace FinanceApp.Shared.Core.Responses
{
    public class Error
    {
        public ErrorType ErrorType { get; set; }
        public string Code { get; set; } = "";
    }
}
