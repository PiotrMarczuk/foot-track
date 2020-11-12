using System;
using System.Collections.Generic;
using System.Linq;
using FootTrack.BusinessLogic.Models.Training;
using FootTrack.BusinessLogic.Models.ValueObjects;
using MongoDB.Bson;
using DBModels = FootTrack.Database.Models;

namespace FootTrack.Repository.Mappers
{
    public static class TrainingMapper
    {
        public static IEnumerable<DBModels.TrainingData> Map(IEnumerable<TrainingRecord> trainingRecords) =>
            trainingRecords.Select(trainingRecord => new DBModels.TrainingData
            {
                Id = ObjectId.GenerateNewId(DateTime.Now),
                Latitude = trainingRecord.Latitude,
                Longitude = trainingRecord.Longitude,
                Speed = trainingRecord.Speed,
                Timestamp = trainingRecord.Timestamp,
            });

        public static TrainingData Map(DBModels.Training trainingRecord) =>
            new TrainingData(
                Id.Create(trainingRecord.Id.ToString()).Value,
                Map(trainingRecord.TrainingData).ToList());

        private static IEnumerable<TrainingRecord> Map(IEnumerable<DBModels.TrainingData> trainingData)
        {
            return trainingData
                .Select(x => new TrainingRecord(x.Latitude, x.Longitude, x.Speed, x.Timestamp));
        }

        public static IEnumerable<Training> Map(IEnumerable<DBModels.Training> trainingRecords) =>
            trainingRecords.Select(x => new Training(Id.Create(x.Id.ToString()).Value, x.Name, x.CreatedAt));
    }
}