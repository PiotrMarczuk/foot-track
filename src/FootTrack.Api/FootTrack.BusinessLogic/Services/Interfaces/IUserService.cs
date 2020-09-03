using System.Threading.Tasks;

using FootTrack.BusinessLogic.Models;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared.Common;

namespace FootTrack.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<AuthenticatedUser>> AuthenticateAsync(UserCredentials userCredentials);

        Task<Result<UserData>> GetByIdAsync(Id id);

        Task<Result<AuthenticatedUser>> RegisterAsync(UserToBeRegistered userToBeRegistered);
    }
}