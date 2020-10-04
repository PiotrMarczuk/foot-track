using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services;
using FootTrack.Communication.Services;
using FootTrack.Repository;
using FootTrack.Shared;

using MongoDB.Bson;

using NSubstitute;

using NUnit.Framework;

using System.Threading.Tasks;

namespace FootTrack.BusinessLogic.Tests.ServiceTests
{
    [TestFixture]
    public class TrainingServiceTests
    {
        private readonly Id _userId = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
        private IUserRepository _userRepository;
        private ITrainingRepository _trainingRepository;
        private IAzureDeviceConnectionService _azureDeviceConnectionService;
        private TrainingService _sut;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _trainingRepository = Substitute.For<ITrainingRepository>();
            _azureDeviceConnectionService = Substitute.For<IAzureDeviceConnectionService>();

            _sut = new TrainingService(_userRepository, _trainingRepository, _azureDeviceConnectionService);
        }

        [Test]
        public async Task When_starting_training_and_user_does_not_exist_should_result_in_error()
        {
            // ARRANGE
            _userRepository.CheckIfUserExist(_userId).Returns(Result.Ok(false));

            // ACT
            var result = await _sut.StartTrainingAsync(_userId);

            // ASSERT
            Assert.That(result.IsFailure);
            Assert.That(result.Error, Is.EqualTo(Errors.General.NotFound()));
        }

        [Test]
        public async Task When_starting_training_and_training_already_begun_should_result_in_error()
        {
            // ARRANGE
            _userRepository.CheckIfUserExist(_userId).Returns(Result.Ok(true));
            _trainingRepository.CheckIfTrainingExist(_userId).Returns(Result.Ok(true));

            // ACT
            var result = await _sut.StartTrainingAsync(_userId);

            // ASSERT
            Assert.That(result.IsFailure);
            Assert.That(result.Error, Is.EqualTo(Errors.Training.AlreadyStarted()));
        }

        [Test]
        public async Task When_starting_training_and_failed_to_connect_to_remote_device_should_result_in_error()
        {
            // ARRANGE
            _userRepository.CheckIfUserExist(_userId).Returns(Result.Ok(true));
            _trainingRepository.CheckIfTrainingExist(_userId).Returns(Result.Ok(false));
            var deviceUnreachableError = Errors.Device.DeviceUnreachable();
            _azureDeviceConnectionService.StartTrainingSessionAsync().Returns(Result.Fail<string>(deviceUnreachableError));

            // ACT
            var result = await _sut.StartTrainingAsync(_userId);

            // ASSERT
            Assert.That(result.IsFailure);
            Assert.That(result.Error, Is.EqualTo(deviceUnreachableError));
        }

        [Test]
        public async Task When_starting_training_and_failed_to_write_state_to_database_should_invoke_end_training_on_remote_device()
        {
            // ARRANGE
            _userRepository.CheckIfUserExist(_userId).Returns(Result.Ok(true));
            _trainingRepository.CheckIfTrainingExist(_userId).Returns(Result.Ok(false));
            const string jobId = "randomJobId";
            _azureDeviceConnectionService.StartTrainingSessionAsync().Returns(Result.Ok(jobId));
            _trainingRepository.BeginTrainingAsync(_userId, jobId).Returns(Result.Fail(Errors.Database.Failed()));
            _azureDeviceConnectionService.EndTrainingSessionAsync(jobId).Returns(Result.Ok());

            // ACT
            var result = await _sut.StartTrainingAsync(_userId);

            // ASSERT
            await _azureDeviceConnectionService.Received().EndTrainingSessionAsync(jobId);
            Assert.That(result.IsFailure);
            Assert.That(result.Error, Is.EqualTo(Errors.Training.FailedToStartTraining()));
        }

        [Test]
        public async Task When_successfully_started_training_should_result_in_starting()
        {
            // ARRANGE
            _userRepository.CheckIfUserExist(_userId).Returns(Result.Ok(true));
            _trainingRepository.CheckIfTrainingExist(_userId).Returns(Result.Ok(false));
            const string jobId = "randomJobId";
            _azureDeviceConnectionService.StartTrainingSessionAsync().Returns(Result.Ok(jobId));
            _trainingRepository.BeginTrainingAsync(_userId, jobId).Returns(Result.Ok());

            // ACT
            var result = await _sut.StartTrainingAsync(_userId);

            // ASSERT
            Assert.That(result.IsSuccess);
        }
    }
}
