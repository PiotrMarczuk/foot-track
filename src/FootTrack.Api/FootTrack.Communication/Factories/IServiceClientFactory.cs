using Microsoft.Azure.Devices;

namespace FootTrack.Communication.Factories
{
    public interface IServiceClientFactory
    {
        ServiceClient Create();
    }
}