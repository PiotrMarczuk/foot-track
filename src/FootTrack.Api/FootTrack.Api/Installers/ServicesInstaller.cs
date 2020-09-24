using FootTrack.BusinessLogic.Services;
using FootTrack.RemoteDevicesConnection;
using FootTrack.RemoteDevicesConnection.Factories;
using FootTrack.RemoteDevicesConnection.Services;
using Microsoft.AspNetCore.Identity;
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
                .AddTransient<IAzureDeviceConnectionService, AzureDeviceConnectionService>();

            services.AddTransient<IServiceClientFactory, ServiceClientFactory>();
            services.AddTransient<ICloudToDeviceMethodFactory, CloudToDeviceMethodFactory>();
        }
    }
}