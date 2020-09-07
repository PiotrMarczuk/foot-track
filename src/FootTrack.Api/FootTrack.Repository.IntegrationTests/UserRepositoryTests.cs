using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.User;
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
    public class UserRepositoryTests
    {
        private DatabaseFixture _dbFixture;
        private ICollectionProvider<User> _collectionProvider;
        private IUserRepository _sut;

        private const string UserEmail = "test@gmail.com";
        private const string UserFirstName = "Kazimierz";
        private const string UserLastName = "Wichura";
        private UserData _insertedUser;

        [SetUp]
        public async Task Setup()
        {
            _dbFixture = new DatabaseFixture();
            _collectionProvider = new CollectionProvider<User>(_dbFixture.MongoDb);
            _sut = new UserRepository(_collectionProvider);

            var insertResult = await InsertUserAsync(UserEmail, UserFirstName, UserLastName);
            _insertedUser = insertResult.Value;
        }

        [TearDown]
        public void Teardown()
        {
            _dbFixture.Dispose();
        }

        [Test]
        public void Should_return_same_user_data_when_adding_new_one()
        {
            // ASSERT
            TestUtils.TestUtils.AssertAreEqualByJson(
                new
                {
                    Email = _insertedUser.Email.Value,
                    _insertedUser.FirstName,
                    _insertedUser.LastName
                },
                new
                {
                    Email = UserEmail,
                    FirstName = UserFirstName,
                    LastName = UserLastName,
                });
        }

        [Test]
        public async Task Should_get_correct_user_data()
        {
            // ACT
            var userDataOrNothing = await _sut.GetUserDataAsync(_insertedUser.Id);
            UserData userData = userDataOrNothing.Value;

            // ASSERT
            TestUtils.TestUtils.AssertAreEqualByJson(userData, _insertedUser);
        }

        [Test]
        public async Task Should_not_return_user_data_when_there_is_no_matching_record()
        {
            // ACT
            var userDataOrNothing = await _sut.GetUserDataAsync(Id.Create(ObjectId.GenerateNewId().ToString()).Value);

            // ASSERT
            Assert.That(userDataOrNothing.HasNoValue);
        }

        [Test]
        public async Task Should_return_user_credentials()
        {
            // ACT
            var userOrNothing = await _sut.GetUserEmailAndHashedPasswordAsync(Email.Create(UserEmail).Value);
            HashedUserCredentials hashedUserCredentials = userOrNothing.Value;
            
            // ASSERT
            TestUtils.TestUtils.AssertAreEqualByJson(
                new
                {
                    hashedUserCredentials.Email,
                    hashedUserCredentials.Id
                },
                new
                {
                    _insertedUser.Email,
                    _insertedUser.Id
                });
        }

        [Test]
        public async Task Should_not_return_user_credentials_when_does_not_exist()
        {
            // ACT
            var userOrNothing =
                await _sut.GetUserEmailAndHashedPasswordAsync(Email.Create("UserEmail@gmail.com").Value);

            // ASSERT
            Assert.That(userOrNothing.HasNoValue);
        }

        [Test]
        public async Task Should_fail_when_adding_user_with_email_that_already_exist()
        {
            // ACT
            var result = await InsertUserAsync(UserEmail, string.Empty, string.Empty);

            // ASSERT
            Assert.That(result.IsFailure);
        }

        private async Task<Result<UserData>> InsertUserAsync(string email, string firstName, string lastName)
        {
            const string hashedPassword = "blablasuperhash";

            HashedUserData hashedUserData = HashedUserData.Create(email, firstName, lastName, hashedPassword).Value;

            return await _sut.InsertUserAsync(hashedUserData);
        }
    }
}