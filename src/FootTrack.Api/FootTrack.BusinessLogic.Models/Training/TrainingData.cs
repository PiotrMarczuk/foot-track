using System.Collections.Generic;
using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models.Training
{
    public sealed class TrainingData
    {
        public Id Id { get; }
        
        public List<TrainingRecord> TrainingRecords { get; }

        public TrainingData(Id id, List<TrainingRecord> trainingRecords)
        {
            Id = id;
            TrainingRecords = trainingRecords;
        }
    }
}