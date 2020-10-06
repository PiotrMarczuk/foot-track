using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.User;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared;

namespace FootTrack.Repository
{
    public interface IUserRepository
    {
        Task<Result<Maybe<HashedUserCredentials>>> GetUserEmailAndHashedPasswordAsync(Email email);

        Task<Result<UserData>> InsertUserAsync(HashedUserData hashedUserData);

        Task<Result<Maybe<UserData>>> GetUserDataAsync(Id id);

        Task<Result<bool>> CheckIfUserExist(Id id);
    }
}