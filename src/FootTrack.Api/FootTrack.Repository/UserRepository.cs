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

        public async Task<Result<Maybe<HashedUserCredentials>>> GetUserEmailAndHashedPasswordAsync(Email email)
        {
            Maybe<HashedUserCredentials> hashedUserCredentialsOrNothing;

            try
            {
                hashedUserCredentialsOrNothing = await _collection
                    .Find(UsersFilters.FilterByEmail(email))
                    .Project(u => HashedUserCredentials.Create(u.Email, u.PasswordHash, u.Id.ToString()).Value)
                    .SingleOrDefaultAsync();
            }
            catch (MongoException)
            {
                return Result.Fail<Maybe<HashedUserCredentials>>(Errors.Database.Failed("Getting user email and password hash"));
            }

            return Result.Ok(hashedUserCredentialsOrNothing);
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

            bool userWithEmailExist;
            try
            {
                userWithEmailExist = await _collection.Find(UsersFilters.FilterByEmail(hashedUserData.Email)).AnyAsync();
            }
            catch(MongoException)
            {
                return Result.Fail<UserData>(Errors.Database.Failed("Checking if user with same email exist"));
            }

            if (userWithEmailExist)
            {
                return Result.Fail<UserData>(Errors.User.EmailIsTaken(hashedUserData.Email.Value));
            }

            try
            {
                await _collection.InsertOneAsync(user);
            }
            catch (MongoException)
            {
                return Result.Fail<UserData>(Errors.Database.Failed("Saving user data"));
            }

            return UserData.Create(user.Id.ToString(), user.Email, user.FirstName, user.LastName);
        }

        public async Task<Result<Maybe<UserData>>> GetUserDataAsync(Id id)
        {
            Maybe<UserData> userDataOrNothing;

            try
            {
                userDataOrNothing = await _collection.Find(DocumentsFilters<User>.FilterById(id))
                    .Project(user =>
                        UserData.Create(user.Id.ToString(), user.Email, user.FirstName, user.LastName).Value)
                    .SingleOrDefaultAsync();
            }
            catch (MongoException)
            {
                return Result.Fail<Maybe<UserData>>(Errors.Database.Failed("Getting user data"));
            }

            return Result.Ok(userDataOrNothing);
        }

        public async Task<Result<bool>> CheckIfUserExist(Id id)
        {
            bool userExist;
            try
            {
                userExist = await _collection.Find(DocumentsFilters<User>.FilterById(id)).AnyAsync();
            }
            catch (MongoException)
            {
                return Result.Fail<bool>(Errors.Database.Failed("Checking if user exist"));
            }

            return Result.Ok(userExist);
        }
    }
}