using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Models.User
{
    public sealed class HashedUserData
    {
        public Email Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string PasswordHash { get; }

        private HashedUserData(
            Email email,
            string firstName,
            string lastName,
            string passwordHash)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
        }

        public static Result<HashedUserData> Create(string email, string firstName, string lastName, string passwordHash)
        {
            Result<Email> emailResult = Email.Create(email);

            return emailResult.IsFailure
                ? Result.Fail<HashedUserData>(emailResult.Error)
                : Result.Ok(new HashedUserData(emailResult.Value, firstName, lastName, passwordHash));
        }
    }
}