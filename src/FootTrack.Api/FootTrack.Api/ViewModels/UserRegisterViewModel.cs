using System.ComponentModel.DataAnnotations;

namespace FootTrack.Api.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Password length should be longer than 6")]
        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
