using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models;
using FootTrack.Database.Providers;
using FootTrack.Repository.Filters;
using FootTrack.Repository.UpdateDefinitions;
using FootTrack.Shared;
using MongoDB.Driver;
using BusinessModels = FootTrack.BusinessLogic.Models.Training;

namespace FootTrack.Repository
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Training> _collection;

        public TrainingRepository(ICollectionProvider<Training> collectionProvider, IMapper mapper)
        {
            _mapper = mapper;
            _collection = collectionProvider.GetCollection();
        }

        public async Task<Result> BeginTrainingAsync(Id userId, string jobId)
        {
            await _collection.InsertOneAsync(new Training
            {
                JobId = jobId,
                State = TrainingState.InProgress,
                UserId = userId.Value,
                TrainingData = new List<TrainingData>(),
            });

            return Result.Ok();
        }

        public async Task<Result<string>> EndTrainingAsync(Id userId)
        {
            Training updateResult;

            try
            {
                updateResult = await _collection.FindOneAndUpdateAsync(
                    TrainingsFilters.FilterByUserIdAndState(userId, TrainingState.InProgress),
                    TrainingsUpdateDefinitions.UpdateTrainingState(TrainingState.Ended));
            }
            catch (MongoException)
            {
                return Result.Fail<string>(Errors.Database.Failed("Ending training"));
            }

            return updateResult != null
                ? Result.Ok(updateResult.JobId)
                : Result.Fail<string>(Errors.General.NotFound("Training"));
        }

        public async Task<Result<bool>> CheckIfTrainingAlreadyStarted(Id userId)
        {
            bool isInProgress;
            try
            {
                isInProgress = await _collection
                    .Find(TrainingsFilters.FilterByUserIdAndState(userId, TrainingState.InProgress))
                    .AnyAsync();
            }
            catch (MongoException)
            {
                return Result.Fail<bool>(Errors.Database.Failed("When looking for active training"));
            }

            return Result.Ok(isInProgress);
        }

        public async Task<Result> AppendTrainingDataAsync(BusinessModels.TrainingData trainingData)
        {
            var trainingRecords = _mapper.Map<List<TrainingData>>(trainingData.TrainingRecords);

            try
            {
                await _collection.UpdateOneAsync(
                    TrainingsFilters.FilterByUserIdAndState(trainingData.UserId, TrainingState.InProgress),
                    TrainingsUpdateDefinitions.PushTrainingRecords(trainingRecords));
            }
            catch (MongoException e)
            {
                Console.WriteLine(e.Message);
                return Result.Fail<bool>(Errors.Database.Failed("When appending training data."));
            }

            return Result.Ok();
        }
    }
}