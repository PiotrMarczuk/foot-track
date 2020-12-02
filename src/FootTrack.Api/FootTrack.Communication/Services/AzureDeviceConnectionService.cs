using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.ValueObjects;
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

        public async Task<Result<Id>> StartTrainingSessionAsync()
        {
            return await _azureDeviceCommunicationFacade
                .InvokeStartTrainingMethodAsync(TargetDevice)
                .OnSuccessAsync(() => _hangfireBackgroundJobFacade.EnqueueJob());
        }

        public async Task<Result> EndTrainingSessionAsync(Id jobId)
        {
            return await _hangfireBackgroundJobFacade
                .DeleteJob(jobId)
                .OnSuccessAsync(() => _azureDeviceCommunicationFacade.InvokeEndTrainingMethodAsync(TargetDevice));
        }
    }
}