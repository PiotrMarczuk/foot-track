using Microsoft.Azure.Devices;

namespace FootTrack.Communication.Factories
{
    public interface ICloudToDeviceMethodFactory
    {
        CloudToDeviceMethod CreateStartMethod();

        CloudToDeviceMethod CreateEndMethod();
    }
}