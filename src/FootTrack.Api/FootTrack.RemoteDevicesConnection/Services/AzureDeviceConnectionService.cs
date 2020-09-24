using System;
using System.Threading.Tasks;
using FootTrack.RemoteDevicesConnection.Factories;
using FootTrack.Shared;
using Microsoft.Azure.Devices;

namespace FootTrack.RemoteDevicesConnection.Services
{
    public class AzureDeviceConnectionService : IAzureDeviceConnectionService
    {
        private readonly ICloudToDeviceMethodFactory _cloudToDeviceMethodFactory;
        private readonly IServiceClientFactory _serviceClientFactory;

        public AzureDeviceConnectionService(
            ICloudToDeviceMethodFactory cloudToDeviceMethodFactory,
            IServiceClientFactory serviceClientFactory)
        {
            _cloudToDeviceMethodFactory = cloudToDeviceMethodFactory;
            _serviceClientFactory = serviceClientFactory;
        }

        private static ServiceClient _sServiceClient;

        private const string TargetDevice = "rpi";

        public async Task<Result> StartTrainingSessionAsync()
        {
            _sServiceClient = _serviceClientFactory.Create();
            CloudToDeviceMethod method = _cloudToDeviceMethodFactory.CreateMethod();

            try
            {
                await _sServiceClient.InvokeDeviceMethodAsync(TargetDevice, method);
            }
            catch (Exception)
            {
                return Result.Fail(Errors.Device.DeviceUnreachable(TargetDevice));
            }

            return Result.Ok();
        }
    }
}