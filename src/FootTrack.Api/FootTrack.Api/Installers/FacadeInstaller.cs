using System;
using FootTrack.Communication.Facades;
using FootTrack.Communication.Factories;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootTrack.Api.Installers
{
    // ReSharper disable once UnusedType.Global
    public class FacadeInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAzureDeviceCommunicationFacade, AzureDeviceCommunicationFacade>(
                AzureDeviceCommunicationFacadeFactoryMethod);

            services.AddTransient<IHangfireBackgroundJobFacade, HangfireBackgroundJobFacade>();
        }

        private static AzureDeviceCommunicationFacade AzureDeviceCommunicationFacadeFactoryMethod(
            IServiceProvider serviceProvider)
        {
            CloudToDeviceMethod cloudToDeviceMethod =
                serviceProvider.GetRequiredService<ICloudToDeviceMethodFactory>().Create();
            ServiceClient serviceClient = serviceProvider.GetRequiredService<IServiceClientFactory>().Create();

            return new AzureDeviceCommunicationFacade(
                serviceClient,
                cloudToDeviceMethod
            );
        }
    }
}