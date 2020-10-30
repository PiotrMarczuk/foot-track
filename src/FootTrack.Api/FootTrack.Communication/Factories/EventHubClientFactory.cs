using System;
using FootTrack.Settings.HubSettings;
using Microsoft.Azure.EventHubs;

namespace FootTrack.Communication.Factories
{
    public class EventHubClientFactory : IEventHubClientFactory
    {
        private readonly IHubSettings _hubSettings;

        public EventHubClientFactory(IHubSettings hubSettings)
        {
            _hubSettings = hubSettings;
        }

        public EventHubClient Create()
        {
            var connectionString = new EventHubsConnectionStringBuilder(
                    new Uri(
                        _hubSettings.EventHubsCompatibleEndpoint),
                    _hubSettings.EventHubsCompatiblePath,
                    _hubSettings.IotHubSasKeyName,
                    _hubSettings.IotHubSasKey)
                .ToString();

            return EventHubClient.CreateFromConnectionString(connectionString);
        }
    }
}