using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.Communication.Services
{
    public interface IAzureDeviceConnectionService
    {
        Task<Result<Id>> StartTrainingSessionAsync();

        Task<Result> EndTrainingSessionAsync(Id jobId);
    }
}