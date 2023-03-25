namespace FinanceApp.Api.Application.Interfaces.Services
{
    public interface IConfigurationService
    {
        string GetConfigVariable(string variableName);
    }
}
