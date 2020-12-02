using System;
using FootTrack.Database.Attributes;
using FootTrack.Database.Contracts;

namespace FootTrack.Database.Models
{
    [BsonCollection(CollectionNames.TrainingData)]
    public class TrainingData : Document.Document
    {
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        
        public double Speed { get; set; }
        
        public DateTime Timestamp { get; set; }
    }
}