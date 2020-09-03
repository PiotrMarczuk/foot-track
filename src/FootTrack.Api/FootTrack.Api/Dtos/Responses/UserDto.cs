using NullGuard;

namespace FootTrack.Api.Dtos.Responses
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Email { get; set; }
        
        [AllowNull]
        public string FirstName { get; set; }

        [AllowNull]
        public string LastName { get; set; }

    }
}
