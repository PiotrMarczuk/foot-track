using System.Threading.Tasks;
using AutoMapper;
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
        private IMapper _mapper;
        private const string JobId = "blablarandomJobId";

        [OneTimeSetUp]
        public void Init()
        {
            _dbFixture = new DatabaseFixture();
        }

        [SetUp]
        public void SetUp()
        {
            _collectionProvider = new CollectionProvider<Training>(_dbFixture.CreateMongoDatabase());
            _mapper = CreateMapper();
            _sut = new TrainingRepository(_collectionProvider, _mapper);
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
            Result result = await _sut.BeginTrainingAsync(_userId, JobId);

            // ASSERT
            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public async Task Should_successfully_check_if_training_exist()
        {
            // ARRANGE
            await _sut.BeginTrainingAsync(_userId, JobId);

            // ACT
            Result<bool> result = await _sut.CheckIfTrainingAlreadyStarted(_userId);

            // ASSERT
            Assert.That(result.Value, Is.True);
        }

        [Test]
        public async Task Should_successfully_check_if_training_does_not_exist()
        {
            // ARRANGE
            await _sut.BeginTrainingAsync(_userId, JobId);

            // ACT
            Result<bool> result = await _sut.CheckIfTrainingAlreadyStarted(Id.Create(ObjectId.Empty.ToString()).Value);

            // ASSERT
            Assert.That(result.Value, Is.False);
        }

        [Test]
        public async Task Should_return_fail_when_ending_training_which_does_not_exist()
        {
            // ACT
            Result<string> result = await _sut.EndTrainingAsync(_userId);

            // ASSERT
            Assert.That(result.IsFailure);
            Assert.That(result.Error, Is.EqualTo(Errors.General.NotFound()));
        }

        [Test]
        public async Task Should_return_jobId_when_ending_training_which_exist()
        {
            // ARRANGE
            await _sut.BeginTrainingAsync(_userId, JobId);

            // ACT
            Result<string> result = await _sut.EndTrainingAsync(_userId);

            // ASSERT
            Assert.That(result.IsSuccess);
            Assert.That(result.Value, Is.EqualTo(JobId));
        }

        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => {});
            return config.CreateMapper();
        }
    }
}
