namespace FinanceApp.Shared.Core.Attributes
{
    public class StatusCodeAttribute : Attribute
    {
        public int StatusCode { get; private set; }

        public StatusCodeAttribute(int statusCode)
        {
            StatusCode = statusCode;
        }
    }
}