using Microsoft.Azure.Devices;

namespace FootTrack.RemoteDevicesConnection.Factories
{
    public interface IServiceClientFactory
    {
        ServiceClient Create();
    }
}