using System;
using System.Threading.Tasks;
using FootTrack.Shared;
using Microsoft.Azure.Devices;

namespace FootTrack.Communication.Services
{
    public class AzureDeviceConnectionService : IAzureDeviceConnectionService
    {
        private const string TargetDevice = "rpi";

        private readonly CloudToDeviceMethod _method;
        private readonly ServiceClient _serviceClient;
        private readonly IJobExecutor _jobExecutor;

        public AzureDeviceConnectionService(
            CloudToDeviceMethod method,
            ServiceClient serviceClient,
            IJobExecutor jobExecutor)
        {
            _method = method;
            _serviceClient = serviceClient;
            _jobExecutor = jobExecutor;
        }

        public async Task<Result> StartTrainingSessionAsync()
        {
            try
            {
                await _serviceClient.InvokeDeviceMethodAsync(TargetDevice, _method);
            }
            catch (Exception)
            {
                return Result.Fail(Errors.Device.DeviceUnreachable(TargetDevice));
            }

            _jobExecutor.Execute();
            return Result.Ok();
        }
    }
}