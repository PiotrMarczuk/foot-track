using System.Threading.Tasks;
using FootTrack.Shared;

namespace FootTrack.Communication.Facades
{
    public interface IAzureDeviceCommunicationFacade
    {
        Task<Result> InvokeStartTrainingMethodAsync(string targetDevice);

        Task<Result> InvokeEndTrainingMethodAsync(string targetDevice);
    }
}