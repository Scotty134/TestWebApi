using Persistence.Abstraction.Repositories;
using Persistence.Repositories;
using Service.Abstraction.Services;
using Service.Services;

namespace TestWebApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
