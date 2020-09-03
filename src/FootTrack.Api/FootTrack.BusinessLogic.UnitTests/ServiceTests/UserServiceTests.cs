using System.Threading.Tasks;

using FootTrack.BusinessLogic.Models;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services.Implementations;
using FootTrack.BusinessLogic.Services.Interfaces;
using FootTrack.Repository;
using FootTrack.Shared.Common;

using Microsoft.AspNetCore.Identity;

using MongoDB.Bson;

using NSubstitute;

using NUnit.Framework;

namespace FootTrack.BusinessLogic.UnitTests.ServiceTests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserService _sut;
        private IUserRepository _userRepository;
        private IPasswordHasher<UserCredentials> _passwordHasher;
        private IJwtTokenService _jwtTokenService;

        private readonly Email _email = Email.Create("test@gmail.com").Value;
        private readonly Password _password = Password.Create("passwordpassword").Value;
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
        public async Task When_called_AuthenticateAsync_and_user_was_not_found_should_fail()
        {
            // ACT
            var result = await _sut.AuthenticateAsync(new UserCredentials(_email, _password));

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo("Email or password is incorrect."));
        }

        [Test]
        public async Task When_called_AuthenticateAsync_and_password_does_not_match_should_fail()
        {
            // ARRANGE
            var userLoginWithHashedPassword = new HashedUserCredentials(
                _email,
                "balaclavas",
                Id.Create(ObjectId.GenerateNewId(1).ToString()).Value);

            _userRepository.GetUserEmailAndHashedPasswordAsync(_email)
                .Returns(userLoginWithHashedPassword);

            // ACT
            var result = await _sut.AuthenticateAsync(new UserCredentials(_email, _password));

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo("Email or password is incorrect."));
        }

        [Test]
        public async Task When_called_AuthenticateAsync_with_correct_data_should_return_authenticate_data()
        {
            // ARRANGE
            var userLoginWithHashedPassword = new HashedUserCredentials(
                _email,
                _password,
                Id.Create(ObjectId.GenerateNewId(1).ToString()).Value);

            _userRepository.GetUserEmailAndHashedPasswordAsync(_email)
                .Returns(userLoginWithHashedPassword);

            _passwordHasher.VerifyHashedPassword(
                    default,
                    userLoginWithHashedPassword.HashedPassword,
                    _password.Value)
                .Returns(PasswordVerificationResult.Success);

            _jwtTokenService.GenerateToken(userLoginWithHashedPassword.Id).Returns("token");

            // ACT
            var result = await _sut.AuthenticateAsync(new UserCredentials(_email, _password));

            // ASSERT
            Assert.That(result.IsSuccess, Is.True);
            Assert.That(result.Value.Email, Is.EqualTo(_email));
            Assert.That(result.Value.Token, Is.Not.Empty);
        }

        [Test]
        public async Task When_called_GetById_with_Id_that_does_not_exist_should_fail()
        {
            // ARRANGE & ACT
            var result = await _sut.GetByIdAsync(Id.Create((ObjectId.GenerateNewId().ToString())).Value);

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.EqualTo("User was not found."));
        }

        [Test]
        public async Task When_called_GetById_with_Id_that_exists_should_return_existing_user()
        {
            // ARRANGE
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;

            _userRepository.GetUserDataAsync(id)
                .Returns(new UserData(id, _email, FirstName, LastName));

            // ACT
            var result = await _sut.GetByIdAsync(id);

            // ASSERT
            Assert.That(result.IsSuccess, Is.True);
            TestUtils.AssertAreEqualByJson(result.Value, new
            {
                Id = id,
                Email = _email,
                FirstName,
                LastName,
            });
        }

        [Test]
        public async Task When_trying_to_register_with_email_already_registered_should_return_error()
        {
            // ARRANGE
            _userRepository.DoesAlreadyExist(_email).Returns(true);
            var userToBeRegistered = new UserToBeRegistered(_email, FirstName, LastName, _password);

            // ACT
            var result = await _sut.RegisterAsync(userToBeRegistered);

            // ASSERT
            Assert.That(result.IsFailure, Is.True);
        }

        [Test]
        public async Task When_trying_to_register_with_email_that_is_not_already_registered_should_register()
        {
            // ARRANGE
            const string mockJwtToken = "babble";
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
            _userRepository.DoesAlreadyExist(_email).Returns(false);
            var userToBeRegistered = new UserToBeRegistered(_email, FirstName, LastName, _password);
            _jwtTokenService.GenerateToken(id).Returns(mockJwtToken);
            _userRepository.InsertUserAsync(default)
                .ReturnsForAnyArgs(Result.Ok(new UserData(id, _email, FirstName, LastName)));

            // ACT
            var result = await _sut.RegisterAsync(userToBeRegistered);

            // ASSERT
            Assert.That(result.IsSuccess, Is.True);
            TestUtils.AssertAreEqualByJson(
                result.Value,
                new
                {
                    Id = id,
                    Email = _email,
                    JwtToken = mockJwtToken,
                });
        }
    }
}