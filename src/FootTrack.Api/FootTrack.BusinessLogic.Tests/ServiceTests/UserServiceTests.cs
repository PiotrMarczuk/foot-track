using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.User;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services;
using FootTrack.Repository;
using FootTrack.Shared;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using NSubstitute;
using NUnit.Framework;

namespace FootTrack.BusinessLogic.UnitTests.ServiceTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _sut;
        private IUserRepository _userRepository;
        private IPasswordHasher<UserCredentials> _passwordHasher;
        private IJwtTokenService _jwtTokenService;

        private const string UserEmail = "test@gmail.com";
        private const string UserPassword = "passwordpassword";
        private const string FirstName = "Jan";
        private const string LastName = "Brzechwa";

        [SetUp]
        public void SetUp()
        {
            _jwtTokenService = Substitute.For<IJwtTokenService>();
            _userRepository = Substitute.For<IUserRepository>();
            _passwordHasher = Substitute.For<IPasswordHasher<UserCredentials>>();

            _sut = new UserService(_passwordHasher, _jwtTokenService, _userRepository);
        }

        [Test]
        public async Task When_tried_to_authenticate_and_user_was_not_found_should_fail()
        {
            // ACT
            Result<AuthenticatedUser> result = await _sut.AuthenticateAsync(UserCredentials.Create(UserEmail, UserPassword).Value);

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(Errors.User.IncorrectEmailOrPassword()));
        }

        [Test]
        public async Task When_tried_to_authenticate_and_password_does_not_match_should_fail()
        {
            // ARRANGE
            HashedUserCredentials userLoginWithHashedPassword = HashedUserCredentials.Create(
                    UserEmail,
                    "balaclavas",
                    Id.Create(ObjectId.GenerateNewId(1).ToString()).Value)
                .Value;

            _userRepository.GetUserEmailAndHashedPasswordAsync(Email.Create(UserEmail).Value)
                .Returns(userLoginWithHashedPassword);

            // ACT
            Result<AuthenticatedUser> result = await _sut.AuthenticateAsync(UserCredentials.Create(UserEmail, UserPassword).Value);

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(Errors.User.IncorrectEmailOrPassword()));
        }

        [Test]
        public async Task When_tried_to_authenticate_with_correct_data_should_pass()
        {
            // ARRANGE
            HashedUserCredentials userLoginWithHashedPassword = HashedUserCredentials.Create(
                    UserEmail,
                    UserPassword,
                    Id.Create(ObjectId.GenerateNewId(1).ToString()).Value)
                .Value;

            _userRepository.GetUserEmailAndHashedPasswordAsync(Email.Create(UserEmail).Value)
                .Returns(userLoginWithHashedPassword);

            _passwordHasher.VerifyHashedPassword(
                    default,
                    userLoginWithHashedPassword.HashedPassword,
                    UserPassword)
                .Returns(PasswordVerificationResult.Success);

            _jwtTokenService.GenerateToken(userLoginWithHashedPassword.Id).Returns("token");

            // ACT
            Result<AuthenticatedUser> result = await _sut.AuthenticateAsync(UserCredentials.Create(UserEmail, UserPassword).Value);

            // ASSERT
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value.Email.Value, Is.EqualTo(UserEmail));
            Assert.That(result.Value.Token, Is.Not.Empty);
        }

        [Test]
        public async Task When_getting_user_with_Id_that_does_not_exist_should_fail()
        {
            // ARRANGE & ACT
            Result<UserData> result = await _sut.GetByIdAsync(Id.Create((ObjectId.GenerateNewId().ToString())).Value);

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo(Errors.General.NotFound()));
        }

        [Test]
        public async Task When_getting_user_with_Id_that_exists_should_return_existing_user()
        {
            // ARRANGE
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
            Email email = Email.Create(UserEmail).Value;

            _userRepository.GetUserDataAsync(id)
                .Returns(UserData.Create(id, UserEmail, FirstName, LastName).Value);

            // ACT
            Result<UserData> result = await _sut.GetByIdAsync(id);

            // ASSERT
            Assert.That(result.IsSuccess, Is.True);
            TestUtils.TestUtils.AssertAreEqualByJson(result.Value, new
            {
                Id = id,
                Email = email,
                FirstName,
                LastName,
            });
        }

        [Test]
        public async Task When_trying_to_register_with_email_that_is_not_already_registered_should_register()
        {
            // ARRANGE
            const string mockJwtToken = "babble";
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
            Email email = Email.Create(UserEmail).Value;
            UserToBeRegistered userToBeRegistered =
                UserToBeRegistered.Create(UserEmail, FirstName, LastName, UserPassword).Value;
            _jwtTokenService.GenerateToken(id).Returns(mockJwtToken);
            _userRepository.InsertUserAsync(default)
                .ReturnsForAnyArgs(UserData.Create(id, UserEmail, FirstName, LastName));

            // ACT
            Result<AuthenticatedUser> result = await _sut.RegisterAsync(userToBeRegistered);

            // ASSERT
            Assert.That(result.IsSuccess, Is.True);
            TestUtils.TestUtils.AssertAreEqualByJson(
                result.Value,
                new
                {
                    Id = id,
                    Email = email,
                    Token = mockJwtToken,
                });
        }
    }
}