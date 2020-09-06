namespace FootTrack.Api.Dtos.Responses
{
    public class AuthenticatedUserDto
    {
        public string Id { get; set; }
        
        public string Email { get; set; }

        public string Token { get; set; }
    }
}
