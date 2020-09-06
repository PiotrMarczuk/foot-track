using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.User;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.BusinessLogic.Services
{
    public interface IUserService
    {
        Task<Result<AuthenticatedUser>> AuthenticateAsync(UserCredentials userCredentials);

        Task<Result<UserData>> GetByIdAsync(Id id);

        Task<Result<AuthenticatedUser>> RegisterAsync(UserToBeRegistered userToBeRegistered);
    }
}