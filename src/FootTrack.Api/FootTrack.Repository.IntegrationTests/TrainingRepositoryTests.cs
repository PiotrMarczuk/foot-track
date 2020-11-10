using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models;
using FootTrack.Database.Providers;
using FootTrack.Shared;
using FootTrack.TestUtils;
using MongoDB.Bson;
using NUnit.Framework;

namespace FootTrack.Repository.IntegrationTests
{
    [TestFixture]
    public class TrainingRepositoryTests
    {
        private DatabaseFixture _dbFixture;
        private ICollectionProvider<Training> _collectionProvider;
        private TrainingRepository _sut;
        private readonly Id _userId = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
        private readonly Id _jobId = Id.Create(ObjectId.GenerateNewId().ToString()).Value;

        [OneTimeSetUp]
        public void Init()
        {
            _dbFixture = new DatabaseFixture();
        }

        [SetUp]
        public void SetUp()
        {
            _collectionProvider = new CollectionProvider<Training>(_dbFixture.CreateMongoDatabase());
            _sut = new TrainingRepository(_collectionProvider);
        }
        
        [TearDown]
        public void Teardown()
        {
            _dbFixture.Dispose();
        }

        [Test]
        public async Task Should_successfully_begin_training()
        {
            // ACT
            Result result = await _sut.BeginTrainingAsync(_userId, _jobId);

            // ASSERT
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public async Task Should_successfully_check_if_training_exist()
        {
            // ARRANGE
            await _sut.BeginTrainingAsync(_userId, _jobId);

            // ACT
            Result<bool> result = await _sut.CheckIfTrainingAlreadyStarted(_userId);

            // ASSERT
            Assert.That(result.Value, Is.True);
        }

        [Test]
        public async Task Should_successfully_check_if_training_does_not_exist()
        {
            // ARRANGE
            await _sut.BeginTrainingAsync(_userId, _jobId);

            // ACT
            Result<bool> result = await _sut.CheckIfTrainingAlreadyStarted(Id.Create(ObjectId.Empty.ToString()).Value);

            // ASSERT
            Assert.That(result.Value, Is.False);
        }

        [Test]
        public async Task Should_return_fail_when_ending_training_which_does_not_exist()
        {
            // ACT
            Result<Id> result = await _sut.EndTrainingAsync(_userId);

            // ASSERT
            Assert.That(result.IsFailure);
            Assert.That(result.Error, Is.EqualTo(Errors.General.NotFound()));
        }

        [Test]
        public async Task Should_return_jobId_when_ending_training_which_exist()
        {
            // ARRANGE
            await _sut.BeginTrainingAsync(_userId, _jobId);

            // ACT
            Result<Id> result = await _sut.EndTrainingAsync(_userId);

            // ASSERT
            Assert.That(result.IsSuccess);
            Assert.That(result.Value, Is.EqualTo(_jobId));
        }
    }
}
