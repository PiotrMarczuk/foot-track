using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models;

using MongoDB.Driver;

namespace FootTrack.Repository.Filters
{
    public static class TrainingsFilters
    {
        public static FilterDefinition<Training> FilterByUserId(Id userId) =>
            Builders<Training>.Filter.Eq(training => training.UserId, userId.Value);

    }
}
