namespace FootTrack.Settings.HubSettings
{
    public class HubSettings : IHubSettings
    {
        public string EventHubsCompatibleEndpoint { get; set; }
        
        public string EventHubsCompatiblePath { get; set; }
        
        public string IotHubSasKeyName { get; set; }
        
        public string IotHubSasKey { get; set; }
    }
}