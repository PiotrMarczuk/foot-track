using Microsoft.Azure.Devices;

namespace FootTrack.RemoteDevicesConnection.Factories
{
    public interface ICloudToDeviceMethodFactory
    {
        CloudToDeviceMethod CreateMethod();
    }
}