using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Communication.Services;
using FootTrack.Repository;
using FootTrack.Shared;

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

        public async Task<Result> StartTrainingAsync(Id userId)
        {
            return await _userRepository.CheckIfUserExist(userId)
                .EnsureAsync(userExist => userExist, Errors.General.NotFound("User", userId.Value))
                .OnSuccessAsync(() => _trainingRepository.CheckIfTrainingExist(userId))
                .EnsureAsync(trainingExist => !trainingExist, Errors.Training.AlreadyStarted(userId))
                .OnSuccessAsync(() => _azureDeviceConnectionService.StartTrainingSessionAsync())
                .OnSuccessAsync(jobId => _trainingRepository.BeginTrainingAsync(userId, jobId)
                    .OnFailureAsync(() => HandleBeginningTrainingSessionFailure(jobId))
                .OnSuccessAsync(Result.Ok));
        }

        private async Task<Result> HandleBeginningTrainingSessionFailure(string jobId)
        {
            await _azureDeviceConnectionService.EndTrainingSessionAsync(jobId);
            return Result.Fail(Errors.Training.FailedToStartTraining());
        }
    }
}