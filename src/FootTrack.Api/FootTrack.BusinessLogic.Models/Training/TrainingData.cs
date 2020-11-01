using System.Collections.Generic;
using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models.Training
{
    public sealed class TrainingData
    {
        public Id UserId { get; }

        public List<TrainingRecord> TrainingRecords { get; }

        public TrainingData(Id userId, List<TrainingRecord> trainingRecords)
        {
            UserId = userId;
            TrainingRecords = trainingRecords;
        }
    }
}