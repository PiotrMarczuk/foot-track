using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FootTrack.Api.Controllers.V1;
using FootTrack.Api.Dtos.Requests;
using FootTrack.BusinessLogic.Models.User;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services;
using FootTrack.Shared;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NSubstitute;
using NUnit.Framework;

namespace FootTrack.Api.UnitTests.ControllersTests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private IUserService _userService;
        private IMapper _mapper;
        private UsersController _sut;

        private const string Email = "test@gmail.com";
        private const string Password = "blabblabla";

        [SetUp]
        public void Setup()
        {
            _userService = Substitute.For<IUserService>();
            _mapper = Substitute.For<IMapper>();

            _sut = new UsersController(_mapper, _userService);
        }

        [Test]
        public async Task When_getting_with_wrong_Id_should_result_in_BadRequest()
        {
            // ARRANGE & ACT
            var result = await _sut.GetById("1") as ObjectResult;

            // ReSharper disable once PossibleInvalidOperationException
            Assert.That((HttpStatusCode)result?.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task When_getting_with_Id_of_existing_user_should_result_in_Ok()
        {
            // ARRANGE
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
            UserData userData = UserData.Create(id.Value, Email, "", "").Value;
            _userService.GetByIdAsync(id).Returns(Result.Ok(userData));

            // ACT
            var result = await _sut.GetById(id) as ObjectResult;

            // ASSERT
            // ReSharper disable once PossibleInvalidOperationException
            Assert.That((HttpStatusCode)result?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task When_getting_with_Id_of_not_existing_user_should_result_in_NotFound()
        {
            // ARRANGE
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
            _userService.GetByIdAsync(id).Returns(Result.Fail<UserData>(Errors.General.NotFound()));

            // ACT
            var result = await _sut.GetById(id) as ObjectResult;

            // ASSERT
            // ReSharper disable once PossibleInvalidOperationException
            Assert.That((HttpStatusCode)result?.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task When_registering_user_that_already_exist_should_result_in_Conflict()
        {
            // ARRANGE
            UserToBeRegistered userToBeRegistered =
                UserToBeRegistered.Create(Email, string.Empty, string.Empty, Password).Value;
            var userRegisterDto = new UserRegisterDto
            {
                Email = Email,
                FirstName = string.Empty,
                LastName = string.Empty,
                Password = Password,
            };

            _userService.RegisterAsync(userToBeRegistered)
                .ReturnsForAnyArgs(Result.Fail<AuthenticatedUser>(Errors.User.EmailIsTaken()));
            
            // ACT
            var result = await _sut.Register(userRegisterDto) as ObjectResult;

            // ASSERT
            // ReSharper disable once PossibleInvalidOperationException
            Assert.That((HttpStatusCode)result?.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task When_registering_user_should_result_in_CreatedAt()
        {
            // ARRANGE
            const string firstName = "firstname";
            const string lastName = "lastname";
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;

            var userRegisterDto = new UserRegisterDto
            {
                Email = Email,
                FirstName = firstName,
                LastName = lastName,
                Password = Password,
            };

            Result<AuthenticatedUser> authenticatedUser =
                AuthenticatedUser.Create(id, Email, "blablablablablablablablablablablablabla");

            _userService.RegisterAsync(default)
                .ReturnsForAnyArgs(authenticatedUser);

            // ACT
            var result = await _sut.Register(userRegisterDto) as ObjectResult;

            // ASSERT
            // ReSharper disable once PossibleInvalidOperationException
            Assert.That((HttpStatusCode)result?.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task When_user_logs_in_with_correct_data_should_return_Ok()
        {
            // ARRANGE
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;

            var userLoginDto = new UserLoginDto
            {
                Email = Email,
                Password = Password,
            };

            Result<AuthenticatedUser> authenticatedUser =
                AuthenticatedUser.Create(id, Email, "blablablablablablablablablablablablabla");

            _userService.AuthenticateAsync(default)
                .ReturnsForAnyArgs(authenticatedUser);

            // ACT
            var result = await _sut.Login(userLoginDto) as ObjectResult;

            // ASSERT
            // ReSharper disable once PossibleInvalidOperationException
            Assert.That((HttpStatusCode)result?.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task When_logging_with_wrong_email_or_password_should_result_in_Unauthorized()
        {
            // ARRANGE
            var userLoginDto = new UserLoginDto
            {
                Email = Email,
                Password = Password,
            };

            _userService.AuthenticateAsync(default)
                .ReturnsForAnyArgs(Result.Fail<AuthenticatedUser>(Errors.User.IncorrectEmailOrPassword()));

            // ACT
            var result = await _sut.Login(userLoginDto) as ObjectResult;

            // ASSERT
            // ReSharper disable once PossibleInvalidOperationException
            Assert.That((HttpStatusCode)result?.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }
    }
}

