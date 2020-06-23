using FootTrack.Api.Models;

namespace FootTrack.Api.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}