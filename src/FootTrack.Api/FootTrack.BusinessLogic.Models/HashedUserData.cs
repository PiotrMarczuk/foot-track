using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models
{
    public class HashedUserData
    {
        public Email Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string PasswordHash { get; }

        public HashedUserData(
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
    }
}