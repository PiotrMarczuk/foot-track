using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Models.User
{
    public sealed class HashedUserCredentials : IUserBasicData
    {
        public Id Id { get; }
        
        public Email Email { get; }
        
        public string HashedPassword { get; }

        private HashedUserCredentials(Email email, string hashedPassword, Id id)
        {
            Email = email;
            HashedPassword = hashedPassword;
            Id = id;
        }

        public static Result<HashedUserCredentials> Create(string email, string hashedPassword, string id)
        {
            var emailResult = Email.Create(email);
            var idResult = Id.Create(id);

            Result validationResult = Result.Combine(emailResult, idResult);

            return validationResult.IsFailure
                ? Result.Fail<HashedUserCredentials>(validationResult.Error)
                : Result.Ok(new HashedUserCredentials(emailResult.Value, hashedPassword, idResult.Value));
        }
    }
}