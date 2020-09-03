using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models
{
    public class AuthenticatedUser : IUserBasicData
    {
        public Id Id { get; }
        
        public Email Email { get; }
        
        public string Token { get; }

        public AuthenticatedUser(Id id, Email email, string token)
        {
            Id = id;
            Email = email;
            Token = token;
        }
    }
}