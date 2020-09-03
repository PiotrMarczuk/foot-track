using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models
{
    public class UserToBeRegistered
    {
        public Email Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public Password Password { get; }

        public UserToBeRegistered(
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
    }
}