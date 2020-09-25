using Microsoft.Azure.EventHubs;

namespace FootTrack.Communication.Factories
{
    public interface IEventHubClientFactory
    {
        EventHubClient Create();
    }
}