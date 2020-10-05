using System;
using FootTrack.BusinessLogic.Services;
using FootTrack.Communication.Factories;
using FootTrack.Communication.Hubs;
using FootTrack.Communication.JobExecutors;
using FootTrack.Communication.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

using Microsoft.Azure.EventHubs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootTrack.Api.Installers
{
    // ReSharper disable once UnusedType.Global
    // ReSharper disable once UnusedMember.Global
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            services
                .AddTransient<IUserService, UserService>();
            services
                .AddTransient<IJwtTokenService, JwtTokenService>();
            services
                .AddTransient<ITrainingService, TrainingService>();
            services
                .AddSingleton<IAzureDeviceConnectionService, AzureDeviceConnectionService>();

            services.AddTransient<IServiceClientFactory, ServiceClientFactory>();
            services.AddTransient<ICloudToDeviceMethodFactory, CloudToDeviceMethodFactory>();
            services.AddTransient<IEventHubClientFactory, EventHubClientFactory>();
            services.AddSingleton<IJobExecutor, TrainingJobExecutor>(TrainingJobExecutorFactoryMethod);
        }

        private static TrainingJobExecutor TrainingJobExecutorFactoryMethod(IServiceProvider serviceProvider)
        {
            EventHubClient eventHubClient = serviceProvider.GetRequiredService<IEventHubClientFactory>().Create();
            var hub = serviceProvider.GetRequiredService<IHubContext<TrainingHub>>();

            return new TrainingJobExecutor(eventHubClient, hub);
        }
    }
}