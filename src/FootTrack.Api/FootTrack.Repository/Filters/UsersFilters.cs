using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models;

using MongoDB.Driver;

namespace FootTrack.Repository.Filters
{
    public static class UsersFilters
    {
        public static FilterDefinition<User> FilterByEmail(Email email) =>
            Builders<User>.Filter.Eq(x => x.Email, email.Value);
    }
}