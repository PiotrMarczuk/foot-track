using System.Collections.Generic;
using FootTrack.Database.Attributes;
using FootTrack.Database.Contracts;
using MongoDB.Bson;

namespace FootTrack.Database.Models
{
    [BsonCollection(CollectionNames.Trainings)]
    public class Training : Document.Document
    {
        public string Name { get; set; }
        
        public ObjectId UserId { get; set; }
        
        public string JobId { get; set; }
        
        public TrainingState State { get; set; }
        
        public List<TrainingData> TrainingData { get; set; }
    }
}