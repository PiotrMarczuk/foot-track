using System.Threading.Tasks;
using FootTrack.Shared;

namespace FootTrack.RemoteDevicesConnection.Services
{
    public interface IAzureDeviceConnectionService
    {
        Task<Result> StartTrainingSessionAsync();
    }
}