using System;
using Microsoft.Azure.Devices;

namespace FootTrack.RemoteDevicesConnection.Factories
{
    public class CloudToDeviceMethodFactory : ICloudToDeviceMethodFactory
    {
        private readonly TimeSpan _responseTimeout = TimeSpan.FromSeconds(30);
        
        public CloudToDeviceMethod CreateMethod()
        {
            return new CloudToDeviceMethod("changeMeasurementState") {ResponseTimeout = _responseTimeout};
        }
    }
}