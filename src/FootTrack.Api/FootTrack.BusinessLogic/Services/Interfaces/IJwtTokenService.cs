using FootTrack.BusinessLogic.Models.ValueObjects;

namespace FootTrack.BusinessLogic.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(Id id);
    }
}