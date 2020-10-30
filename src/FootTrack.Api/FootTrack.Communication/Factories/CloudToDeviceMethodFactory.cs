using System;
using Microsoft.Azure.Devices;

namespace FootTrack.Communication.Factories
{
    public class CloudToDeviceMethodFactory : ICloudToDeviceMethodFactory
    {
        private readonly TimeSpan _responseTimeout = TimeSpan.FromSeconds(30);

        public CloudToDeviceMethod CreateStartMethod()
        {
            return new CloudToDeviceMethod("startTraining") {ResponseTimeout = _responseTimeout};
        }

        public CloudToDeviceMethod CreateEndMethod()
        {
            return new CloudToDeviceMethod("endTraining") {ResponseTimeout = _responseTimeout};
        }
    }
}