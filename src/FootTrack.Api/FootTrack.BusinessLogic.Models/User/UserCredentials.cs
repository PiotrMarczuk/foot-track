using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Models.User
{
    public sealed class UserCredentials
    {
        public Email Email { get; }

        public Password Password { get; }

        private UserCredentials(Email email, Password password)
        {
            Email = email;
            Password = password;
        }

        public static Result<UserCredentials> Create(string email, string password)
        {
            var emailResult = Email.Create(email);
            var passwordResult = Password.Create(password);

            Result validationResult = Result.Combine(emailResult, passwordResult);

            return validationResult.IsSuccess
                ? Result.Ok(new UserCredentials(Email.Create(email).Value, Password.Create(password).Value))
                : Result.Fail<UserCredentials>(validationResult.Error);
        }
    }
}