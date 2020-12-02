using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FootTrack.Repository.Filters
{
    public static class TrainingsFilters
    {
        public static FilterDefinition<Training> FilterByUserId(Id userId) => 
            Builders<Training>.Filter.Eq(training => training.UserId, ObjectId.Parse(userId));
        
        public static FilterDefinition<Training> FilterByUserIdAndState(Id userId, TrainingState trainingState) =>
            Builders<Training>.Filter.And(FilterByUserId(userId), FilterByState(trainingState));

        private static FilterDefinition<Training> FilterByState(TrainingState trainingState) =>
            Builders<Training>.Filter.Eq(training => training.State, trainingState);
    }
}
