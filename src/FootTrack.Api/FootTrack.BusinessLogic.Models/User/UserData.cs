using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Models.User
{
    public sealed class UserData : IUserBasicData
    {
        public Id Id { get; }

        public Email Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        private UserData(Id id, Email email, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public static Result<UserData> Create(string id, string email, string firstName, string lastName)
        {
            Result<Id> idResult = Id.Create(id);
            Result<Email> emailResult = Email.Create(email);

            Result validationResult = Result.Combine(idResult, emailResult);

            return validationResult.IsFailure 
                ? Result.Fail<UserData>(validationResult.Error) 
                : Result.Ok(new UserData(idResult.Value, emailResult.Value, firstName, lastName));
        }
    }
}