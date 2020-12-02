using System;
using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Models.Training
{
    public class Training
    {
        public Id Id { get; }

        public string Name { get; }

        public DateTime DateAndTime { get; }

        public Training(Id id, string name, DateTime dateAndTime)
        {
            Id = id;
            Name = name;
            DateAndTime = dateAndTime;
        }
    }
}