using System;
using Microsoft.Azure.Devices;

namespace FootTrack.Communication.Factories
{
    public class CloudToDeviceMethodFactory : ICloudToDeviceMethodFactory
    {
        private readonly TimeSpan _responseTimeout = TimeSpan.FromSeconds(30);

        public CloudToDeviceMethod Create()
        {
            return new CloudToDeviceMethod("changeMeasurementState") {ResponseTimeout = _responseTimeout};
        }
    }
}