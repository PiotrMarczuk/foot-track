using NullGuard;

namespace FootTrack.Api.Dtos.Requests
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
