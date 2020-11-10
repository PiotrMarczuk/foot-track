using System.Collections.Generic;
using FootTrack.Communication.Dtos;

namespace FootTrack.Api.Dtos.Responses
{
    public class TrainingDataDto
    {
        public string Id { get; set; }
        

        public string Name { get; set; }
        
        public string UserId { get; set; }
        

        public List<TrainingRecordDto> TrainingRecords { get; set; }
    }
}