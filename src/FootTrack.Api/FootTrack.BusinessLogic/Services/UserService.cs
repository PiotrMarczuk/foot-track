using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.User;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Repository;
using FootTrack.Shared;
using FootTrack.Shared.ExtensionMethods;
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

        public async Task<Result<AuthenticatedUser>> AuthenticateAsync(UserCredentials userCredentials) =>
            await _userRepository.GetUserEmailAndHashedPasswordAsync(userCredentials.Email)
                .OnSuccessAsync(maybeUser => maybeUser.ToResult(Errors.User.IncorrectEmailOrPassword()))
                .EnsureAsync(user =>
                        CheckIfPasswordMatch(user.HashedPassword, userCredentials.Password),
                    Errors.User.IncorrectEmailOrPassword())
                .OnSuccessAsync(hashedUserCredentials => Result.Ok(CreateAuthenticatedUser(hashedUserCredentials)));

        public async Task<Result<UserData>> GetByIdAsync(Id id) =>
            await _userRepository.GetUserDataAsync(id)
                .OnSuccessAsync(userDataOrNothing =>
                    userDataOrNothing.ToResult(Errors.General.NotFound("User", id.Value)));

        public async Task<Result<AuthenticatedUser>> RegisterAsync(UserToBeRegistered userToBeRegistered)
        {
            string hashedPassword = _passwordHasher.HashPassword(default, userToBeRegistered.Password);

            return await _userRepository.InsertUserAsync(HashedUserData.Create(
                    userToBeRegistered.Email,
                    userToBeRegistered.FirstName,
                    userToBeRegistered.LastName,
                    hashedPassword).Value)
                .OnSuccessAsync(userData => Result.Ok(CreateAuthenticatedUser(userData)));
        }

        private AuthenticatedUser CreateAuthenticatedUser(IUserBasicData user)
        {
            string token = _jwtTokenService.GenerateToken(user.Id);
            return AuthenticatedUser.Create(user.Id, user.Email, token).Value;
        }

        private bool CheckIfPasswordMatch(string hashedPassword, Password actual) =>
            _passwordHasher
                .VerifyHashedPassword(
                    default,
                    hashedPassword,
                    actual) == PasswordVerificationResult.Success;
    }
}