using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models
{
    public class UserData : IUserBasicData
    {
        public Id Id { get; }

        public Email Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public UserData(Id id, Email email, string firstName, string lastName)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}