using System;
using System.Threading.Tasks;
using FootTrack.Shared;
using Microsoft.Azure.Devices;

namespace FootTrack.Communication.Facades
{
    public class AzureDeviceCommunicationFacade : IAzureDeviceCommunicationFacade
    {
        private readonly ServiceClient _serviceClient;
        private readonly CloudToDeviceMethod _method;

        public AzureDeviceCommunicationFacade(ServiceClient serviceClient, CloudToDeviceMethod method)
        {
            _serviceClient = serviceClient;
            _method = method;
        }

        public async Task<Result> InvokeChangeStateMethodAsync(string targetDevice)
        {
            try
            {
                await _serviceClient.InvokeDeviceMethodAsync(targetDevice, _method);
            }
            catch (Exception)
            {
                return Result.Fail<string>(Errors.Device.DeviceUnreachable(targetDevice));
            }

            return Result.Ok();
        }
    }
}