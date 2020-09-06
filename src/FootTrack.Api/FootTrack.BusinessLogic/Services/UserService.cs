using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.User;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Repository;
using FootTrack.Shared;
using Microsoft.AspNetCore.Identity;

namespace FootTrack.BusinessLogic.Services
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
                .ToResultAsync(Errors.User.IncorrectEmailOrPassword())
                .EnsureAsync(user =>
                        CheckIfPasswordMatch(user.HashedPassword, userCredentials.Password),
                    Errors.User.IncorrectEmailOrPassword())
                .OnSuccessAsync(CreateAuthenticatedUser);
        }

        public async Task<Result<UserData>> GetByIdAsync(Id id)
        {
            return await _userRepository.GetUserDataAsync(id)
                .ToResultAsync(Errors.General.NotFound("User", id.Value));
        }

        public async Task<Result<AuthenticatedUser>> RegisterAsync(UserToBeRegistered userToBeRegistered)
        {
            string hashedPassword = _passwordHasher.HashPassword(default, userToBeRegistered.Password);

            return await _userRepository.InsertUserAsync(HashedUserData.Create(
                    userToBeRegistered.Email,
                    userToBeRegistered.FirstName,
                    userToBeRegistered.LastName,
                    hashedPassword).Value)
                .OnSuccessAsync(CreateAuthenticatedUser);
        }

        private AuthenticatedUser CreateAuthenticatedUser(IUserBasicData user)
        {
            string token = _jwtTokenService.GenerateToken(user.Id);
            return AuthenticatedUser.Create(user.Id, user.Email, token).Value;
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