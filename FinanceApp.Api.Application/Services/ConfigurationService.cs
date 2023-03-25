using FinanceApp.Api.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace FinanceApp.Api.Application.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _config;

        public ConfigurationService(IConfiguration config)
        {
            _config = config;
        }

        public string GetConfigVariable(string variableName)
        {
            return _config.GetSection(variableName)?.Value ?? "";
        }
    }
}
