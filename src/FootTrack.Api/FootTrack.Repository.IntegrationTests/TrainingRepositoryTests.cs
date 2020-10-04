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
            var result = await _sut.CheckIfTrainingExist(_userId);

            // ASSERT
            Assert.That(result.Value, Is.True);
        }

        [Test]
        public async Task Should_successfully_check_if_training_does_not_exist()
        {
            // ARRANGE
            await _sut.BeginTrainingAsync(_userId, JobId);

            // ACT
            var result = await _sut.CheckIfTrainingExist(Id.Create(ObjectId.Empty.ToString()).Value);

            // ASSERT
            Assert.That(result.Value, Is.False);
        }
    }
}
