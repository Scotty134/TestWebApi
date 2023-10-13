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
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IPhotoService, PhotoService>();
            services.AddSingleton<ILikesService, LikeService>();
            services.AddSingleton<IMessageRepository, MessageRepository>();

            return services;
        }

        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPhotoRepository, PhotoRespository>();
            services.AddSingleton<ILikesRepository, LikesRepository>();
            services.AddSingleton<IMessageService, MessageService>();

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
