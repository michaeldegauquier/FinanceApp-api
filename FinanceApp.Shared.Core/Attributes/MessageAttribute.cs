namespace FinanceApp.Shared.Core.Attributes
{
    public class MessageAttribute : Attribute
    {
        public string Message { get; private set; }

        public MessageAttribute(string message)
        {
            Message = message;
        }
    }
}
