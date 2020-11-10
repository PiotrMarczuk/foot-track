using System.Collections.Generic;
using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.Training;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Communication.Services;
using FootTrack.Repository;
using FootTrack.Shared;
using FootTrack.Shared.ExtensionMethods;

namespace FootTrack.BusinessLogic.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITrainingRepository _trainingRepository;
        private readonly IAzureDeviceConnectionService _azureDeviceConnectionService;

        public TrainingService(
            IUserRepository userRepository,
            ITrainingRepository trainingRepository,
            IAzureDeviceConnectionService azureDeviceConnectionService)
        {
            _userRepository = userRepository;
            _trainingRepository = trainingRepository;
            _azureDeviceConnectionService = azureDeviceConnectionService;
        }

        public async Task<Result> StartTrainingAsync(Id userId) =>
            await _userRepository.CheckIfUserExist(userId)
                .EnsureAsync(userExist => userExist, Errors.General.NotFound("User", userId.Value))
                .OnSuccessAsync(() => _trainingRepository.CheckIfTrainingAlreadyStarted(userId))
                .EnsureAsync(trainingAlreadyStarted => !trainingAlreadyStarted, Errors.Training.AlreadyStarted(userId))
                .OnSuccessAsync(() => _azureDeviceConnectionService.StartTrainingSessionAsync())
                .OnSuccessAsync(jobId => _trainingRepository.BeginTrainingAsync(userId, jobId)
                    .OnFailureAsync(() => HandleBeginningTrainingSessionFailure(userId, jobId)));

        public async Task<Result> EndTrainingAsync(Id userId) =>
            await _trainingRepository.EndTrainingAsync(userId)
                .OnSuccessAsync(jobId => _azureDeviceConnectionService.EndTrainingSessionAsync(jobId)
                    .OnFailureAsync(() => HandleEndingTrainingSessionFailure(userId, jobId)));

        public async Task<Result> AppendTrainingDataAsync(TrainingData trainingData) =>
            await _trainingRepository.AppendTrainingDataAsync(trainingData);

        public async Task<Result<IEnumerable<TrainingData>>> GetTrainings(
            GetTrainingsForUserParameters trainingsForUserParametersData) =>
            await _trainingRepository.GetTrainingsForUser(trainingsForUserParametersData);

        public async Task<Result<TrainingData>> GetTraining(Id trainingId) =>
            await _trainingRepository.GetTraining(trainingId);

        private async Task<Result> HandleEndingTrainingSessionFailure(Id userId, Id jobId)
        {
            await _trainingRepository.BeginTrainingAsync(userId, jobId);
            return Result.Fail(Errors.Training.FailedToEndTraining(userId));
        }

        private async Task<Result> HandleBeginningTrainingSessionFailure(Id userId, Id jobId)
        {
            await _azureDeviceConnectionService.EndTrainingSessionAsync(jobId);
            return Result.Fail(Errors.Training.FailedToStartTraining(userId));
        }
    }
}