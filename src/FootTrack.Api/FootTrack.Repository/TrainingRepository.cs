using System.Threading.Tasks;

using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models;
using FootTrack.Database.Providers;
using FootTrack.Repository.Filters;
using FootTrack.Repository.UpdateDefinitions;
using FootTrack.Shared;

using MongoDB.Driver;

namespace FootTrack.Repository
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly IMongoCollection<Training> _collection;

        public TrainingRepository(ICollectionProvider<Training> collectionProvider)
        {
            _collection = collectionProvider.GetCollection();
        }

        public async Task<Result> BeginTrainingAsync(Id userId, string jobId)
        {
            await _collection.InsertOneAsync(new Training
            {
                JobId = jobId,
                State = TrainingState.InProgress,
                UserId = userId.Value,
            });

            return Result.Ok();
        }

        public async Task<Result<string>> EndTrainingAsync(Id userId)
        {
            Training updateResult = await _collection.FindOneAndUpdateAsync(
                TrainingsFilters.FilterByUserId(userId),
                TrainingsUpdateDefinitions.UpdateTrainingState(TrainingState.Ended));

            return updateResult != null
                ? Result.Ok(updateResult.JobId)
                : Result.Fail<string>(Errors.General.NotFound("Training"));
        }

        public async Task<Result<bool>> CheckIfTrainingExist(Id userId)
        {
            return Result.Ok(await _collection
                .Find(TrainingsFilters.FilterByUserId(userId))
                .AnyAsync());
        }
    }
}