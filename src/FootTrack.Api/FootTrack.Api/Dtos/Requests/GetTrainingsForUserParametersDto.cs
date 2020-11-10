using System.ComponentModel.DataAnnotations;
using FootTrack.Api.Attributes;

namespace FootTrack.Api.Dtos.Requests
{
    public class GetTrainingsForUserParametersDto
    {
        [Id, Required]
        public string UserId { get; set; }
        
        [Required]
        public int PageNumber { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}