using System;
using System.Threading.Tasks;
using FootTrack.Shared;
using Microsoft.Azure.Devices;

namespace FootTrack.Communication.Facades
{
    public class AzureDeviceCommunicationFacade : IAzureDeviceCommunicationFacade
    {
        private readonly ServiceClient _serviceClient;
        private readonly CloudToDeviceMethod _startTrainingMethod;
        private readonly CloudToDeviceMethod _endTrainingMethod;

        public AzureDeviceCommunicationFacade(
            ServiceClient serviceClient, 
            CloudToDeviceMethod startTrainingMethod,
            CloudToDeviceMethod endTrainingMethod)
        {
            _serviceClient = serviceClient;
            _startTrainingMethod = startTrainingMethod;
            _endTrainingMethod = endTrainingMethod;
        }

        public async Task<Result> InvokeStartTrainingMethodAsync(string targetDevice)
        {
            try
            {
                await _serviceClient.InvokeDeviceMethodAsync(targetDevice, _startTrainingMethod);
            }
            catch (Exception)
            {
                return Result.Fail<string>(Errors.Device.DeviceUnreachable(targetDevice));
            }

            return Result.Ok();
        }

        public async Task<Result> InvokeEndTrainingMethodAsync(string targetDevice)
        {
            try
            {
                await _serviceClient.InvokeDeviceMethodAsync(targetDevice, _endTrainingMethod);
            }
            catch (Exception)
            {
                return Result.Fail<string>(Errors.Device.DeviceUnreachable(targetDevice));
            }

            return Result.Ok();
        }
    }
}