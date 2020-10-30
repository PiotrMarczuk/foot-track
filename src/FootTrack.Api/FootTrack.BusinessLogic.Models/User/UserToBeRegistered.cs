using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Models.User
{
    public sealed class UserToBeRegistered
    {
        public Email Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Password Password { get; }

        private UserToBeRegistered(
            Email email,
            string firstName,
            string lastName,
            Password password)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }

        public static Result<UserToBeRegistered> Create(
            string email,
            string firstName,
            string lastName,
            string password)
        {
            Result<Email> emailResult = Email.Create(email);
            Result<Password> passwordResult = Password.Create(password);

            Result validationResult = Result.Combine(emailResult, passwordResult);

            return validationResult.IsFailure
                ? Result.Fail<UserToBeRegistered>(validationResult.Error)
                : Result.Ok(new UserToBeRegistered(emailResult.Value, firstName, lastName, passwordResult.Value));
        }
    }
}