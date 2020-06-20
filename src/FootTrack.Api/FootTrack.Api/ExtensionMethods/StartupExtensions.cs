using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using FootTrack.Api.Attributes;
using FootTrack.Api.Mappings;
using FootTrack.Api.Repositories.Implementations;
using FootTrack.Api.Repositories.Interfaces;
using FootTrack.Api.Services.Implementations;
using FootTrack.Api.Services.Interfaces;
using FootTrack.Api.Settings.JwtToken;
using FootTrack.Api.Settings.MongoDb;

namespace FootTrack.Api.ExtensionMethods
{
    public static class StartupExtensions
    {

        public static void LoadConfigs(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddSingleton<IJwtTokenSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<JwtTokenSettings>>().Value);
        }

        public static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.Configure<JwtTokenSettings>(configuration.GetSection("JwtTokenSettings"));
        }

        public static void ServicesConfiguration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<ModelValidationFilterAttribute>();
        }

        public static void ConfigureMapper(this IServiceCollection services)
        {
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }).CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
