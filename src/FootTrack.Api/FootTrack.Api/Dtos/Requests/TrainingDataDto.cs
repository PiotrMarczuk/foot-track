using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FootTrack.Api.Attributes;
using FootTrack.Communication.Dtos;

namespace FootTrack.Api.Dtos.Requests
{
    public class TrainingDataDto
    {
        [Id, Required]
        public string UserId { get; set; }
        
        [Required]
        public List<TrainingRecordDto> TrainingRecords { get; set; }
    }
}