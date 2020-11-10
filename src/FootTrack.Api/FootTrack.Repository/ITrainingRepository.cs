using System.Collections.Generic;
using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.Training;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.Repository
{
    public interface ITrainingRepository
    {
        Task<Result<Id>> BeginTrainingAsync(Id userId, Id jobId);

        Task<Result<Id>> EndTrainingAsync(Id userId);

        Task<Result<bool>> CheckIfTrainingAlreadyStarted(Id userId);

        Task<Result> AppendTrainingDataAsync(TrainingData trainingData);
        
        Task<Result<IEnumerable<TrainingData>>> GetTrainingsForUser(GetTrainingsForUserParameters trainingsForUserParametersData);
        
        Task<Result<TrainingData>> GetTraining(Id trainingId);
    }
}