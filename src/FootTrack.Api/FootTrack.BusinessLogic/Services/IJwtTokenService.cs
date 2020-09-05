using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(Id id);
    }
}