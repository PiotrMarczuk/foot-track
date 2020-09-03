using System.Threading.Tasks;

using FootTrack.BusinessLogic.Models;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services.Interfaces;
using FootTrack.Repository;
using FootTrack.Shared.Common;

using Microsoft.AspNetCore.Identity;

namespace FootTrack.BusinessLogic.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher<UserCredentials> _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;

        public UserService(
            IPasswordHasher<UserCredentials> passwordHasher,
            IJwtTokenService jwtTokenService,
            IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }

        public async Task<Result<AuthenticatedUser>> AuthenticateAsync(UserCredentials userCredentials)
        {
            return await _userRepository.GetUserEmailAndHashedPasswordAsync(userCredentials.Email)
                .ToResultAsync("Email or password is incorrect.")
                .EnsureAsync(user =>
                        CheckIfPasswordMatch(user.HashedPassword, userCredentials.Password),
                    "Email or password is incorrect.")
                .OnSuccessAsync(CreateAuthenticatedUser);
        }

        public async Task<Result<UserData>> GetByIdAsync(Id id)
        {
            return await _userRepository.GetUserDataAsync(id)
                .ToResultAsync("User was not found.");
        }

        public async Task<Result<AuthenticatedUser>> RegisterAsync(UserToBeRegistered userToBeRegistered)
        {
            bool doesAlreadyExist = await _userRepository.DoesAlreadyExist(userToBeRegistered.Email);

            if (doesAlreadyExist)
            {
                return Result.Fail<AuthenticatedUser>(
                    $"User with provided email: {userToBeRegistered.Email} already exists");
            }

            string hashedPassword = _passwordHasher.HashPassword(default, userToBeRegistered.Password);

            return await _userRepository.InsertUserAsync(new HashedUserData(
                    userToBeRegistered.Email,
                    userToBeRegistered.FirstName,
                    userToBeRegistered.LastName,
                    hashedPassword))
                .OnSuccessAsync(CreateAuthenticatedUser);
        }

        private AuthenticatedUser CreateAuthenticatedUser(IUserBasicData user)
        {
            string token = _jwtTokenService.GenerateToken(user.Id);
            return new AuthenticatedUser(user.Id, user.Email, token);
        }

        private bool CheckIfPasswordMatch(string hashedPassword, Password actual)
        {
            return _passwordHasher
                .VerifyHashedPassword(
                    default,
                    hashedPassword,
                    actual) == PasswordVerificationResult.Success;
        }
    }
}