using FootTrack.BusinessLogic.Services;
using FootTrack.Settings;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootTrack.Api.Installers
{
    // ReSharper disable once UnusedType.Global
    // ReSharper disable once UnusedMember.Global
    public class CacheInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();
            configuration
                .GetSection(nameof(RedisCacheSettings))
                .Bind(redisCacheSettings);

            services
                .AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled)
            {
                return;
            }

            services.AddStackExchangeRedisCache(
                options =>
                    options.Configuration = redisCacheSettings.ConnectionString);

            services
                .AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
