using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models
{
    public class HashedUserCredentials : IUserBasicData
    {
        public Id Id { get; }
        
        public Email Email { get; }
        
        public string HashedPassword { get; }

        public HashedUserCredentials(Email email, string hashedPassword, Id id)
        {
            Email = email;
            HashedPassword = hashedPassword;
            Id = id;
        }
    }
}