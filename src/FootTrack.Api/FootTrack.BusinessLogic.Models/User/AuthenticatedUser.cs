using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Models.User
{
    public sealed class AuthenticatedUser
    {
        public Id Id { get; }

        public Email Email { get; }

        public string Token { get; }

        private AuthenticatedUser(Id id, Email email, string token)
        {
            Id = id;
            Email = email;
            Token = token;
        }

        public static Result<AuthenticatedUser> Create(string id, string email, string token)
        {
            Result<Id> idResult = Id.Create(id);
            Result<Email> emailResult = Email.Create(email);

            Result validationResult = Result.Combine(idResult, emailResult);

            return validationResult.IsFailure
                ? Result.Fail<AuthenticatedUser>(validationResult.Error)
                : Result.Ok(new AuthenticatedUser(idResult.Value, emailResult.Value, token));
        }
    }
}