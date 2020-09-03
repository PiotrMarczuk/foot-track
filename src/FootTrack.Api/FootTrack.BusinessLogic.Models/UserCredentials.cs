using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models
{
    public class UserCredentials
    {
        public Email Email { get; }
        
        public Password Password { get; }

        public UserCredentials(Email email, Password password)
        {
            Email = email;
            Password = password;
        }
    }
}