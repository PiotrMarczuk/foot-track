using System.ComponentModel.DataAnnotations;
using FootTrack.Api.Attributes;

namespace FootTrack.Api.Dtos.Requests
{
    public class UserLoginDto
    {
        [Email, Required]
        public string Email { get; set; }


        [Password, Required]
        public string Password { get; set; }
    }
}
