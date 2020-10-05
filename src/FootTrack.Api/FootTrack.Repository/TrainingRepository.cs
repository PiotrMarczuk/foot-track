using System;
using System.Threading.Tasks;

using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models;
using FootTrack.Database.Providers;
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

        public Task<Result> EndTrainingAsync(Id userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> CheckIfTrainingExist(Id userId)
        {
            return Result.Ok(await _collection
                .Find(
                    Builders<Training>
                        .Filter
                        .Eq(training => training.UserId, userId.Value))
                .AnyAsync());
        }
    }
}