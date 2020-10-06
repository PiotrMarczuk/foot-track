using FootTrack.Communication.Facades;
using FootTrack.Communication.Services;
using FootTrack.Shared;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;

namespace FootTrack.Communication.UnitTests.Services
{
    [TestFixture]
    public class AzureDeviceConnectionServiceTests
    {
        private IAzureDeviceCommunicationFacade _azureDeviceCommunicationFacade;
        private IHangfireBackgroundJobFacade _hangfireBackgroundJobFacade;
        private AzureDeviceConnectionService _sut;

        [SetUp]
        public void SetUp()
        {
            _azureDeviceCommunicationFacade = Substitute.For<IAzureDeviceCommunicationFacade>();
            _hangfireBackgroundJobFacade = Substitute.For<IHangfireBackgroundJobFacade>();
            _sut = new AzureDeviceConnectionService(_azureDeviceCommunicationFacade, _hangfireBackgroundJobFacade);
        }

        [Test]
        public async Task When_failed_to_invoke_remote_method_should_return_fail_result()
        {
            // ARRANGE
            Error error = Errors.Device.DeviceUnreachable("rpi");
            _azureDeviceCommunicationFacade.InvokeStartTrainingMethodAsync("rpi")
                .Returns(Result.Fail(error));

            // ACT
            Result<string> result = await _sut.StartTrainingSessionAsync();

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(error));
        }

        [Test]
        public async Task When_succeeded_to_invoke_remote_method_should_return_ok_result()
        {
            // ARRANGE
            _azureDeviceCommunicationFacade.InvokeStartTrainingMethodAsync("rpi")
                .Returns(Result.Ok());
            _hangfireBackgroundJobFacade.EnqueueJob().Returns(Result.Ok("jobId"));


            // ACT
            Result<string> result = await _sut.StartTrainingSessionAsync();

            // ASSERT
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public async Task When_failed_to_delete_job_should_return_fail_result()
        {
            // ARRANGE
            const string jobId = "jobId";
            Error error = Errors.General.Empty();

            _hangfireBackgroundJobFacade.DeleteJob(jobId).Returns(Result.Fail(error));

            // ACT
            Result result = await _sut.EndTrainingSessionAsync(jobId);

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(error));
        }

        [Test]
        public async Task When_failed_to_end_training_session_should_return_fail_result()
        {
            // ARRANGE
            const string jobId = "jobId";
            Error error = Errors.Device.DeviceUnreachable("rpi");
            _hangfireBackgroundJobFacade.DeleteJob(jobId).Returns(Result.Ok());
            _azureDeviceCommunicationFacade.InvokeStartTrainingMethodAsync("rpi")
                .Returns(Result.Fail(error));

            // ACT
            Result result = await _sut.EndTrainingSessionAsync(jobId);

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(error));
        }

        [Test]
        public async Task When_succeeded_to_end_training_should_return_ok_result()
        {
            // ARRANGE
            const string jobId = "jobId";
            _hangfireBackgroundJobFacade.DeleteJob(jobId).Returns(Result.Ok());
            _azureDeviceCommunicationFacade.InvokeStartTrainingMethodAsync("rpi")
                .Returns(Result.Ok());

            // ACT
            Result result = await _sut.EndTrainingSessionAsync(jobId);

            // ASSERT
            Assert.That(result.IsSuccess, Is.True);
        }
    }
}
