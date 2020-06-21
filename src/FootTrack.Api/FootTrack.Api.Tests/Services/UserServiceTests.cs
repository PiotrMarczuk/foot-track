using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using NSubstitute.ReturnsExtensions;

using FootTrack.Api.Exceptions;
using FootTrack.Api.Models;
using FootTrack.Api.Repositories.Interfaces;
using FootTrack.Api.Services.Implementations;
using FootTrack.Api.Services.Interfaces;
using FootTrack.Api.Settings.JwtToken;
using FootTrack.Api.ViewModels;

namespace FootTrack.Api.Tests.Services
{
    public class UserServiceTests
    {
        private IUserService _sut;
        private IMongoRepository<User> _usersRepository;
        private IMapper _mapper;
        private IPasswordHasher<User> _passwordHasher;
        private IJwtTokenService _tokenService;
        private User _existingUser;

        private const string ExistingEmail = "test@test.com";
        private const string CorrectPassword = "CorrectPassword";
        private const string WrongPassword = "WrongPassword";

        private readonly UserRegisterViewModel _notExistingUserForRegister = new UserRegisterViewModel
        {
            Email = "testcreation@test.com",
            Password = "test",
            FirstName = "Jerry",
            LastName = "Super"
        };

        [SetUp]
        public void Setup()
        {
            _passwordHasher = new PasswordHasher<User>();
            _existingUser = SetupExistingUser();
            _mapper = SetupMapper();
            _tokenService = SetupTokenService();
            _usersRepository = SetupMongoRepository();

            _sut = new UserService(_passwordHasher, _mapper, _tokenService, _usersRepository);
        }

        [Test]
        public void When_given_null_parameter_should_throw_argument_null_exception()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _sut.AuthenticateAsync(null));
            Assert.ThrowsAsync<ArgumentNullException>(() => _sut.AuthenticateAsync(null));
        }

        [Test]
        [TestCase("", "    ")]
        [TestCase("    ", "    ")]
        [TestCase("", "")]
        [TestCase("    ", "    ")]
        [TestCase("test", "")]
        public void When_given_empty_or_white_space_email_or_password_should_throw_argument_exception(string email, string password)
        {
            var loginViewModel = new UserLoginViewModel
            {
                Email = email,
                Password = password
            };

            var registerViewModel = new UserRegisterViewModel()
            {
                Email = email,
                Password = password
            };


            Assert.ThrowsAsync<ArgumentException>(() => _sut.AuthenticateAsync(loginViewModel));

            Assert.ThrowsAsync<ArgumentException>(() => _sut.CreateAsync(registerViewModel));
        }

        [Test]
        public void When_trying_to_create_user_with_email_already_in_system_should_throw_AlreadyExistsException()
        {
            var viewModel = new UserRegisterViewModel
            {
                Email = ExistingEmail,
                Password = WrongPassword
            };

            Assert.ThrowsAsync<AlreadyExistsException>(() => _sut.CreateAsync(viewModel));
        }

        [Test]
        public void When_provided_not_existing_email_should_throw_NotFoundException()
        {
            var viewModel = new UserLoginViewModel
            {
                Email = _notExistingUserForRegister.Email,
                Password = WrongPassword
            };

            SetUserRepositoryToReturnNullWhenFinding();

            Assert.ThrowsAsync<NotFoundException>(() => _sut.AuthenticateAsync(viewModel));
        }

        [Test]
        public async Task When_creating_new_user_should_return_not_null_and_add_password_hash()
        {
            SetUserRepositoryToReturnNullWhenFinding();

            var result = await _sut.CreateAsync(_notExistingUserForRegister);

            Assert.That(result, Is.Not.EqualTo(null));
            Assert.That(result.PasswordHash, Is.Not.Empty);
        }

        [Test]
        public void When_provided_wrong_password_on_existing_should_throw_argument_exception()
        {
            var loginViewModel = new UserLoginViewModel
            {
                Email = ExistingEmail,
                Password = WrongPassword
            };

            Assert.ThrowsAsync<ArgumentException>(() => _sut.AuthenticateAsync(loginViewModel));
        }

        [Test]
        public async Task When_provided_correct_credentials_should_return_object_with_token()
        {
            var loginViewModel = new UserLoginViewModel
            {
                Email = ExistingEmail,
                Password = CorrectPassword
            };

            var result = await _sut.AuthenticateAsync(loginViewModel);

            Assert.That(result.Token, Is.Not.Empty);
        }

        [Test]
        [TestCase("")]
        [TestCase("   ")]
        public void When_getting_user_with_empty_or_whitespace_should_throw_ArgumentException(string id)
        {
            Assert.ThrowsAsync<ArgumentException>(() => _sut.GetByIdAsync(id));
        }

        [Test]
        [TestCase(null)]
        public void When_getting_user_with_null_should_throw_ArgumentNullException(string id)
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _sut.GetByIdAsync(id));
        }

        [Test]
        [TestCase("12345")]
        public void When_getting_existing_user_with_incorrect_id_should_throw_NotFoundException(string id)
        {
            _usersRepository.FindByIdAsync("1234").Returns(Task.FromResult(new User()));

            Assert.ThrowsAsync<NotFoundException>(() => _sut.GetByIdAsync(id));
        }

        [Test]
        public void When_getting_existing_user_with_correct_id_should_return_not_null_object()
        {
            const string existingId = "1234";
            _usersRepository.FindByIdAsync(existingId).Returns(Task.FromResult(new User()));

            var result = _sut.GetByIdAsync(existingId);

            Assert.That(result, Is.Not.Null);
        }

        private User SetupExistingUser()
        {
            var user = new User
            {
                Email = ExistingEmail
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, CorrectPassword);

            return user;
        }

        private void SetUserRepositoryToReturnNullWhenFinding()
        {
            _usersRepository.FindOneAsync(Arg.Any<Expression<Func<User, bool>>>()).ReturnsNullForAnyArgs();
        }

        private static IJwtTokenService SetupTokenService()
        {
            var settings = Substitute.For<IJwtTokenSettings>();
            settings.Secret.Returns("testesttesttesttestesttesttesttestesttesttesttestesttesttest");

            return new JwtTokenService(settings);
        }

        private IMongoRepository<User> SetupMongoRepository()
        {
            var repository = Substitute.For<IMongoRepository<User>>();
            repository.FindOneAsync(Arg.Any<Expression<Func<User, bool>>>()).ReturnsForAnyArgs(Task.FromResult(_existingUser));

            return repository;
        }

        private IMapper SetupMapper()
        {
            var mapper = Substitute.For<IMapper>();

            mapper.Map<User>(Arg.Any<UserRegisterViewModel>()).Returns(new User
            {
                Email = _notExistingUserForRegister.Email,
                FirstName = _notExistingUserForRegister.FirstName,
                LastName = _notExistingUserForRegister.LastName,
            });

            mapper.Map<AuthenticatedUserViewModel>(Arg.Any<User>()).Returns(new AuthenticatedUserViewModel());

            return mapper;
        }
    }
}