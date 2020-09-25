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
        private readonly IAzureDeviceConnectionService _azureDeviceConnectionService;

        public TrainingService(
            IUserRepository userRepository,
            IAzureDeviceConnectionService azureDeviceConnectionService)
        {
            _userRepository = userRepository;
            _azureDeviceConnectionService = azureDeviceConnectionService;
        }

        public async Task<Result> StartTrainingAsync(Id userId)
        {
            if (!await _userRepository.CheckIfUserExist(userId))
            {
                return Result.Fail(Errors.General.NotFound("User", userId.Value));
            }

            Result<string> trainingSessionResult = await _azureDeviceConnectionService
                .StartTrainingSessionAsync();

            return trainingSessionResult.IsSuccess ? Result.Ok() : Result.Fail(trainingSessionResult.Error);
        }
    }
}