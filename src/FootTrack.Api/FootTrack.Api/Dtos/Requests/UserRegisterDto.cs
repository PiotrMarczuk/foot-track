using System.ComponentModel.DataAnnotations;
using FootTrack.Api.Attributes;

namespace FootTrack.Api.Dtos.Requests
{
    public class UserRegisterDto
    {
        [Email, Required]
        public string Email { get; set; }

        [Password, Required]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
