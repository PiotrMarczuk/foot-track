using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Settings.MongoDb;
using FootTrack.Shared.Common;
using MongoDB.Bson;
using MongoDB.Driver;
using User = FootTrack.Database.Models.User;

namespace FootTrack.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IMongoDbSettings settings) : base(settings)
        {
        }

        public async Task<Maybe<HashedUserCredentials>> GetUserEmailAndHashedPasswordAsync(Email email)
        {
            return await Collection
                .Find(Builders<User>.Filter.Eq(user => user.Email, email.Value))
                .Project(u => new HashedUserCredentials((Email) u.Email, u.PasswordHash, (Id) u.Id.ToString()))
                .SingleOrDefaultAsync();
        }

        public async Task<bool> DoesAlreadyExist(Email email)
        {
            return await Collection.Find(Builders<User>.Filter.Eq(user => user.Email, email.Value))
                .AnyAsync();
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

            await Collection.InsertOneAsync(user);

            return Result.Ok(new UserData((Id) user.Id.ToString(), (Email) user.Email, user.FirstName, user.LastName));
        }

        public async Task<Maybe<UserData>> GetUserDataAsync(Id id)
        {
            return await Collection.Find(Builders<User>.Filter.Eq(user => user.Id, ObjectId.Parse(id)))
                .Project(user =>
                    new UserData((Id) user.Id.ToString(), (Email) user.Email, user.FirstName, user.LastName))
                .SingleOrDefaultAsync();
        }
    }
}