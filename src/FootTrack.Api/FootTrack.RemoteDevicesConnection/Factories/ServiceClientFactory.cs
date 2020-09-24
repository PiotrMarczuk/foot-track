using FootTrack.Settings.AzureServiceClient;
using Microsoft.Azure.Devices;

namespace FootTrack.RemoteDevicesConnection.Factories
{
    public class ServiceClientFactory : IServiceClientFactory
    {
        private readonly IAzureServiceClientSettings _settings;

        public ServiceClientFactory(IAzureServiceClientSettings settings)
        {
            _settings = settings;
        }

        public ServiceClient Create()
        {
            return ServiceClient.CreateFromConnectionString(_settings.AzureServiceClientConnectionString);
        }
    }
}