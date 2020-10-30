namespace FootTrack.Settings.HubSettings
{
    public interface IHubSettings
    {
        string EventHubsCompatibleEndpoint { get; }
        
        string EventHubsCompatiblePath { get; }
        
        string IotHubSasKeyName { get; }
        
        string IotHubSasKey { get; }
    }
}