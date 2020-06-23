using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using Microsoft.AspNetCore.Identity;

using FootTrack.Api.Exceptions;
using FootTrack.Api.Models;
using FootTrack.Api.Repositories.Interfaces;
using FootTrack.Api.Services.Interfaces;
using FootTrack.Api.ViewModels;

namespace FootTrack.Api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMongoRepository<User> _usersRepository;

        public UserService(
            IPasswordHasher<User> passwordHasher,
            IMapper mapper,
            IJwtTokenService jwtTokenService,
            IMongoRepository<User> usersRepository)
        {
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _usersRepository = usersRepository;
        }

        public async Task<AuthenticatedUserViewModel> AuthenticateAsync(UserLoginViewModel loginViewModel)
        {
            Guard.Argument(loginViewModel, nameof(loginViewModel)).NotNull();
            Guard.Argument(loginViewModel.Email, nameof(loginViewModel.Email))
                .NotNull()
                .NotEmpty()
                .NotWhiteSpace();
            Guard.Argument(loginViewModel.Password, nameof(loginViewModel.Password))
                .NotNull()
                .NotEmpty()
                .NotWhiteSpace();

            var user = await _usersRepository
                .FindOneAsync(u => u.Email == loginViewModel.Email);

            if (user == null)
            {
                throw new WrongCredentialsException("Username or password is incorrect.");
            }

            if (_passwordHasher
                    .VerifyHashedPassword(
                        user,
                        user.PasswordHash,
                        loginViewModel.Password) == PasswordVerificationResult.Failed)
            {
                throw new WrongCredentialsException("Username or password is incorrect");
            }

            var userViewModel = _mapper.Map<AuthenticatedUserViewModel>(user);
            userViewModel.Token = _jwtTokenService
                .GenerateToken(user);

            return userViewModel;
        }

        public async Task<User> CreateAsync(UserRegisterViewModel registerViewModel)
        {
            Guard.Argument(registerViewModel, nameof(registerViewModel)).NotNull();
            Guard.Argument(registerViewModel.Email, nameof(registerViewModel.Email))
                .NotNull()
                .NotEmpty()
                .NotWhiteSpace();
            Guard.Argument(registerViewModel.Password, nameof(registerViewModel.Password))
                .NotNull()
                .NotEmpty()
                .NotWhiteSpace();

            if (await _usersRepository
                    .FindOneAsync(
                        u => u.Email == registerViewModel.Email) != null)
            {
                throw new AlreadyExistsException($"There's already an account with email {registerViewModel.Email}");
            }

            var user = _mapper.Map<User>(registerViewModel);
            user.PasswordHash = _passwordHasher
                .HashPassword(user, registerViewModel.Password);

            await _usersRepository.InsertOneAsync(user);

            return user;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            Guard.Argument(id, nameof(id))
                .NotEmpty()
                .NotWhiteSpace()
                .NotNull();

            var user = await _usersRepository
                .FindByIdAsync(id);

            return user ?? throw new NotFoundException("User not found.");
        }
    }
}
