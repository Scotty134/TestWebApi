using Persistence.Abstraction.Repositories;
using Persistence.Repositories;
using Service.Abstraction.Services;
using Service.Services;
using TestWebApi.Helpers;
using TestWebApi.Interfaces;
using TestWebApi.Services;

namespace TestWebApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPhotoService, PhotoService>();

            return services;
        }

        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPhotoRepository, PhotoRespository>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();            
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ICloudPhotoService, CloudPhotoService>();
            services.AddScoped<LogUserActivity>();

            return services;
        }
    }
}
