using System.ComponentModel.DataAnnotations;
using FootTrack.Api.Attributes;

namespace FootTrack.Api.Dtos.Requests
{
    public class IdDto
    {
        [Id, Required]
        public string Id { get; set; }
    }
}