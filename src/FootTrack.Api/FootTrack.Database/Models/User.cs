using FootTrack.Database.Attributes;
using FootTrack.Database.Contracts;

namespace FootTrack.Database.Models
{
    [BsonCollection(CollectionNames.Users)]
    public class User : Document.Document
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}
