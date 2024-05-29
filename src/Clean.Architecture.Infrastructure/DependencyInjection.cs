using Clean.Architecture.Core.Common.Interfaces.Authentication;
using Clean.Architecture.Core.Common.Utility;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.Core.Services.Implementation;
using Clean.Architecture.Core.Services.Interfaces;
using Clean.Architecture.Infrastructure.Authentication;
using Clean.Architecture.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Clean.Architecture.Infrastructure
{
    public static class DependencyInjection
    {
        /*public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }*/

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration
            )
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            //services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}
