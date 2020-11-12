using System.Collections.Generic;
using FootTrack.Communication.Dtos;

namespace FootTrack.Api.Dtos.Responses
{
    public class TrainingDataDto
    {
        public List<TrainingRecordDto> TrainingRecords { get; set; }
    }
}