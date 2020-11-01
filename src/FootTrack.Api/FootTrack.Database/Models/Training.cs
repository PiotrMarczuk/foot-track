using System.Collections.Generic;
using FootTrack.Database.Attributes;
using FootTrack.Database.Contracts;

namespace FootTrack.Database.Models
{
    [BsonCollection(CollectionNames.Trainings)]
    public class Training : Document.Document
    {
        public string UserId { get; set; }
        
        public string JobId { get; set; }
        
        public TrainingState State { get; set; }
        
        public List<TrainingData> TrainingData { get; set; }
    }

    public enum TrainingState
    {
        Ended = 0,
        InProgress = 1,
    }
}