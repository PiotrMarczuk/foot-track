using System;

namespace FootTrack.BusinessLogic.Models.Training
{
    public sealed class TrainingRecord
    {
        public double Latitude { get; }

        public double Longitude { get; }

        public double Speed { get; }

        public DateTime Timestamp { get; }


        public TrainingRecord(double latitude, double longitude, double speed, DateTime timestamp)
        {
            Latitude = latitude;
            Longitude = longitude;
            Speed = speed;
            Timestamp = timestamp;
        }
    }
}