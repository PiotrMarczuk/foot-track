using System.Collections.Generic;
using System.Threading.Tasks;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.Database.Models;
using FootTrack.Database.Providers;
using FootTrack.Repository.Filters;
using FootTrack.Repository.Mappers;
using FootTrack.Repository.UpdateDefinitions;
using FootTrack.Shared;
using MongoDB.Bson;
using MongoDB.Driver;
using BusinessModels = FootTrack.BusinessLogic.Models.Training;

namespace FootTrack.Repository
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly IMongoCollection<Training> _collection;

        public TrainingRepository(ICollectionProvider<Training> collectionProvider)
        {
            _collection = collectionProvider.GetCollection();
        }

        public async Task<Result<Id>> BeginTrainingAsync(Id userId, Id jobId)
        {
            var training = new Training
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Training " + await _collection.EstimatedDocumentCountAsync() + 1,
                JobId = jobId,
                State = TrainingState.InProgress,
                UserId = ObjectId.Parse(userId.Value),
                TrainingData = new List<TrainingData>(),
            };

            await _collection.InsertOneAsync(training);

            Id trainingId = Id.Create(training.Id.ToString()).Value;
            return Result.Ok(trainingId);
        }

        public async Task<Result<Id>> EndTrainingAsync(Id userId)
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
                return Result.Fail<Id>(Errors.Database.Failed("Ending training"));
            }

            return updateResult != null
                ? Result.Ok(Id.Create(updateResult.JobId).Value)
                : Result.Fail<Id>(Errors.General.NotFound("Training"));
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
                return Result.Fail<bool>(Errors.Database.Failed("looking for active training"));
            }

            return Result.Ok(isInProgress);
        }

        public async Task<Result> AppendTrainingDataAsync(BusinessModels.TrainingData trainingData)
        {
            IEnumerable<TrainingData> trainingRecords = TrainingMapper.Map(trainingData.TrainingRecords);

            try
            {
                await _collection.UpdateOneAsync(
                    DocumentsFilters<Training>.FilterById(trainingData.Id),
                    TrainingsUpdateDefinitions.PushTrainingRecords(trainingRecords));
            }
            catch (MongoException)
            {
                return Result.Fail<bool>(Errors.Database.Failed("appending training data."));
            }

            return Result.Ok();
        }


        public async Task<Result<IEnumerable<BusinessModels.Training>>> GetTrainingsForUser(
            BusinessModels.GetTrainingsForUserParameters trainingsForUserParametersData)
        {
            var trainings = new List<BusinessModels.Training>();

            try
            {
                List<Training> dbTrainings = await _collection.Find(
                        TrainingsFilters.FilterByUserId(trainingsForUserParametersData.UserId))
                    .Skip((trainingsForUserParametersData.PageNumber - 1) * trainingsForUserParametersData.PageSize)
                    .Limit(trainingsForUserParametersData.PageSize)
                    .ToListAsync();

                trainings.AddRange(TrainingMapper.Map(dbTrainings));
            }
            catch (MongoException)
            {
                return Result.Fail<IEnumerable<BusinessModels.Training>>(
                    Errors.Database.Failed("getting trainings"));
            }

            return Result.Ok((IEnumerable<BusinessModels.Training>)trainings);
        }

        public async Task<Result<BusinessModels.TrainingData>> GetTraining(Id trainingId)
        {
            BusinessModels.TrainingData trainingData;

            try
            {
                Training dbTraining = await _collection
                    .Find(DocumentsFilters<Training>.FilterById(trainingId))
                    .SingleAsync();

                trainingData = TrainingMapper.Map(dbTraining);
            }
            catch (MongoException)
            {
                return Result.Fail<BusinessModels.TrainingData>(
                    Errors.Database.Failed("getting training data"));
            }

            return Result.Ok(trainingData);
        }
    }
}