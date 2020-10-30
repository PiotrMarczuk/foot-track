using FootTrack.Database.Models;

using MongoDB.Driver;

namespace FootTrack.Repository.UpdateDefinitions
{
    public static class TrainingsUpdateDefinitions
    {
        public static UpdateDefinition<Training> UpdateTrainingState(TrainingState trainingState) =>
            Builders<Training>.Update.Set(training => training.State, trainingState);

    }
}
