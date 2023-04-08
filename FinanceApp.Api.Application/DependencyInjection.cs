using FinanceApp.Api.Application.Common.Behaviours;
using FinanceApp.Api.Application.Interfaces.Repositories;
using FinanceApp.Api.Application.Interfaces.Services.Authentication;
using FinanceApp.Api.Application.Repositories.IncomeExpenseRepository;
using FinanceApp.Api.Application.Repositories.TagRepository;
using FinanceApp.Api.Application.Services.Authentication;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FinanceApp.Api.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            services.AddTransient<IIncomeExpenseRepository, IncomeExpenseRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IClaimsService, ClaimsService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
