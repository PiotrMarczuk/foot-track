using System;

namespace FootTrack.Communication.Dtos
{
    public class TrainingRecordDto
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Speed { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
