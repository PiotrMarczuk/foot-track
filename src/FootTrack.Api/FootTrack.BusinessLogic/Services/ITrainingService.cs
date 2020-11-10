using System.Collections.Generic;
using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.Training;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Services
{
    public interface ITrainingService
    {
        Task<Result> StartTrainingAsync(Id userId);

        Task<Result> EndTrainingAsync(Id userId);
        
        Task<Result> AppendTrainingDataAsync(TrainingData trainingData);
        
        Task<Result<IEnumerable<TrainingData>>> GetTrainings(GetTrainingsForUserParameters trainingsForUserParameters);
        
        Task <Result<TrainingData>> GetTraining(Id trainingId);
    }
}