using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.User;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models;
using FootTrack.Database.Providers;
using FootTrack.Repository.Filters;
using FootTrack.Shared;
using MongoDB.Driver;

namespace FootTrack.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(ICollectionProvider<User> collectionProvider)
        {
            _collection = collectionProvider.GetCollection();
        }

        public async Task<Maybe<HashedUserCredentials>> GetUserEmailAndHashedPasswordAsync(Email email)
        {
            return await _collection
                .Find(UserFilter.FilterByEmail(email))
                .Project(u => HashedUserCredentials.Create(u.Email, u.PasswordHash, u.Id.ToString()).Value)
                .SingleOrDefaultAsync();
        }

        public async Task<Result<UserData>> InsertUserAsync(HashedUserData hashedUserData)
        {
            var user = new User
            {
                Email = hashedUserData.Email,
                FirstName = hashedUserData.FirstName,
                LastName = hashedUserData.LastName,
                PasswordHash = hashedUserData.PasswordHash,
            };

            if (await DoesAlreadyExist(hashedUserData.Email))
            {
                return Result.Fail<UserData>(Errors.User.EmailIsTaken(hashedUserData.Email.Value));
            }

            await _collection.InsertOneAsync(user);

            return UserData.Create(user.Id.ToString(), user.Email, user.FirstName, user.LastName);
        }

        public async Task<Maybe<UserData>> GetUserDataAsync(Id id)
        {
            return await _collection.Find(DocumentFilter<User>.FilterById(id))
                .Project(user =>
                    UserData.Create(user.Id.ToString(), user.Email, user.FirstName, user.LastName).Value)
                .SingleOrDefaultAsync();
        }

        private async Task<bool> DoesAlreadyExist(Email email)
        {
            return await _collection.Find(UserFilter.FilterByEmail(email))
                .AnyAsync();
        }
    }
}