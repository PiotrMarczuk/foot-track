using FootTrack.Settings.JwtToken;
using FootTrack.Settings.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FootTrack.Api.Installers
{
    public class SettingsInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider
                    .GetRequiredService<IOptions<MongoDbSettings>>()
                    .Value);

            services.AddSingleton<IJwtTokenSettings>(serviceProvider =>
                serviceProvider
                    .GetRequiredService<IOptions<JwtTokenSettings>>()
                    .Value);

            services
                .Configure<MongoDbSettings>(configuration
                    .GetSection(nameof(MongoDbSettings)));
            services
                .Configure<JwtTokenSettings>(configuration
                    .GetSection(nameof(JwtTokenSettings)));
        }
    }
}
