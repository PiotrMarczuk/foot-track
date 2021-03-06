﻿using FootTrack.Settings.AzureServiceClient;
using FootTrack.Settings.HubSettings;
using FootTrack.Settings.JwtToken;
using FootTrack.Settings.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FootTrack.Api.Installers
{
    // ReSharper disable once UnusedType.Global
    // ReSharper disable once UnusedMember.Global
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

            services.AddSingleton<IAzureServiceClientSettings>(serviceProvider =>
                serviceProvider
                    .GetRequiredService<IOptions<AzureServiceClientSettings>>()
                    .Value);

            services.AddSingleton<IHubSettings>(serviceProvider =>
                serviceProvider
                    .GetRequiredService<IOptions<HubSettings>>()
                    .Value);
            
            services
                .Configure<MongoDbSettings>(configuration
                    .GetSection(nameof(MongoDbSettings)));
            services
                .Configure<JwtTokenSettings>(configuration
                    .GetSection(nameof(JwtTokenSettings)));
            services
                .Configure<AzureServiceClientSettings>(configuration
                    .GetSection(nameof(AzureServiceClientSettings)));
            services
                .Configure<HubSettings>(configuration
                    .GetSection(nameof(HubSettings)));
        }
    }
}