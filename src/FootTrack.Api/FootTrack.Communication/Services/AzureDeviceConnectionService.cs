using System.Threading.Tasks;

using FootTrack.Communication.Facades;
using FootTrack.Shared;
using FootTrack.Shared.ExtensionMethods;

namespace FootTrack.Communication.Services
{
    public class AzureDeviceConnectionService : IAzureDeviceConnectionService
    {
        private readonly IAzureDeviceCommunicationFacade _azureDeviceCommunicationFacade;
        private readonly IHangfireBackgroundJobFacade _hangfireBackgroundJobFacade;
        private const string TargetDevice = "rpi";

        public AzureDeviceConnectionService(
            IAzureDeviceCommunicationFacade azureDeviceCommunicationFacade,
            IHangfireBackgroundJobFacade hangfireBackgroundJobFacade)
        {
            _azureDeviceCommunicationFacade = azureDeviceCommunicationFacade;
            _hangfireBackgroundJobFacade = hangfireBackgroundJobFacade;
        }

        public async Task<Result<string>> StartTrainingSessionAsync()
        {
            return await _azureDeviceCommunicationFacade
                .InvokeChangeStateMethodAsync(TargetDevice)
                .OnSuccessAsync(() => _hangfireBackgroundJobFacade.EnqueueJob())
                .OnSuccessAsync(Result.Ok);
        }

        public async Task<Result> EndTrainingSessionAsync(string jobId)
        {
            return await _hangfireBackgroundJobFacade
                .DeleteJob(jobId)
                .OnSuccessAsync(() => _azureDeviceCommunicationFacade.InvokeChangeStateMethodAsync(TargetDevice));
        }
    }
}