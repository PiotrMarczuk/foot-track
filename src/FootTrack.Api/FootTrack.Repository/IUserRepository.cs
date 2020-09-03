using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Shared.Common;

namespace FootTrack.Repository
{
    public interface IUserRepository
    {
        Task<Maybe<HashedUserCredentials>> GetUserEmailAndHashedPasswordAsync(Email email);

        Task<bool> DoesAlreadyExist(Email email);

        Task<Result<UserData>> InsertUserAsync(HashedUserData hashedUserData);

        Task<Maybe<UserData>> GetUserDataAsync(Id id);
    }
}