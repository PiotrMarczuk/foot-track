using FootTrack.Api.Attributes;

namespace FootTrack.Api.Models
{
    [BsonCollection("users")]
    public class User : Document.Document
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
