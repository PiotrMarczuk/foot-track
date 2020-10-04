using System;
using System.Threading.Tasks;
using FootTrack.Shared;
using Hangfire;
using Microsoft.Azure.Devices;

namespace FootTrack.Communication.Services
{
    public class AzureDeviceConnectionService : IAzureDeviceConnectionService
    {
        private const string TargetDevice = "rpi";

        private readonly CloudToDeviceMethod _method;
        private readonly ServiceClient _serviceClient;

        public AzureDeviceConnectionService(
            CloudToDeviceMethod method,
            ServiceClient serviceClient)
        {
            _method = method;
            _serviceClient = serviceClient;
        }

        public async Task<Result<string>> StartTrainingSessionAsync()
        {
            try
            {
                await _serviceClient.InvokeDeviceMethodAsync(TargetDevice, _method);
            }
            catch (Exception)
            {
                return Result.Fail<string>(Errors.Device.DeviceUnreachable(TargetDevice));
            }

            string jobId = BackgroundJob.Enqueue<IJobExecutor>(jobExecutor => jobExecutor.Execute());
            return Result.Ok(jobId);
        }

        public async Task<Result> EndTrainingSessionAsync(string jobId)
        {
            BackgroundJob.Delete(jobId);
            try
            {
                await _serviceClient.InvokeDeviceMethodAsync(TargetDevice, _method);
            }
            catch (Exception)
            {
                return Result.Fail(Errors.Device.DeviceUnreachable(TargetDevice));
            }

            return Result.Ok();
        }
    }
}