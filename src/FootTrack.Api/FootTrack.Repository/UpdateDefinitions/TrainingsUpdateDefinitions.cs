using System.Collections.Generic;
using FootTrack.Database.Models;

using MongoDB.Driver;

namespace FootTrack.Repository.UpdateDefinitions
{
    public static class TrainingsUpdateDefinitions
    {
        public static UpdateDefinition<Training> UpdateTrainingState(TrainingState trainingState) =>
            Builders<Training>.Update.Set(training => training.State, trainingState);

        public static UpdateDefinition<Training> PushTrainingRecords(IEnumerable<TrainingData> trainingData) =>
            Builders<Training>.Update.PushEach(training => training.TrainingData, trainingData);
    }
}
