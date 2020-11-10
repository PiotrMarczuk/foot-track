using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FootTrack.Api.Attributes;
using FootTrack.Communication.Dtos;

namespace FootTrack.Api.Dtos.Requests
{
    public class AppendTrainingDataDto
    {
        [Id, Required]
        public string TrainingId { get; set; }
        
        [Required]
        public List<TrainingRecordDto> TrainingRecords { get; set; }
    }
}