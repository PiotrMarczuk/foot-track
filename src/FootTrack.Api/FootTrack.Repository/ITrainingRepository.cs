using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.Training;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.Repository
{
    public interface ITrainingRepository
    {
        Task<Result> BeginTrainingAsync(Id userId, string jobId);

        Task<Result<string>> EndTrainingAsync(Id userId);

        Task<Result<bool>> CheckIfTrainingAlreadyStarted(Id userId);

        Task<Result> AppendTrainingDataAsync(TrainingData trainingData);
    }
}