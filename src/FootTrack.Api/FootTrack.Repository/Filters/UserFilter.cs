using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models;

using MongoDB.Driver;

namespace FootTrack.Repository.Filters
{
    public static class UserFilter
    {
        public static FilterDefinition<User> FilterByEmail(Email email)
        {
            return Builders<User>.Filter.Eq(x => x.Email, email.Value);
        }
    }
}