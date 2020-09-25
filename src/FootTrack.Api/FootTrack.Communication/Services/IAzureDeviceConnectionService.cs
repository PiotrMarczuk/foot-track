using System.Threading.Tasks;
using FootTrack.Shared;

namespace FootTrack.Communication.Services
{
    public interface IAzureDeviceConnectionService
    {
        Task<Result<string>> StartTrainingSessionAsync();
    }
}