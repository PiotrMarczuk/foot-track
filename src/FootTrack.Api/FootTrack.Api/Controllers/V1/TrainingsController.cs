using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FootTrack.Api.Contracts.V1;
using FootTrack.Api.Dtos.Requests;
using FootTrack.Api.Dtos.Responses;
using FootTrack.BusinessLogic.Models.Training;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services;
using FootTrack.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FootTrack.Api.Controllers.V1
{
    public class TrainingsController : BaseController
    {
        private readonly ITrainingService _trainingService;

        public TrainingsController(IMapper mapper, ITrainingService trainingService) : base(mapper)
        {
            _trainingService = trainingService;
        }

        [HttpPost(ApiRoutes.Trainings.Start)]
        public async Task<IActionResult> Start([FromBody] IdDto userIdDto)
        {
            Id userId = Id.Create(userIdDto.Id).Value;

            Result startTrainingResult = await _trainingService.StartTrainingAsync(userId);

            return OkOrError(startTrainingResult);
        }

        [HttpPost(ApiRoutes.Trainings.End)]
        public async Task<IActionResult> End([FromBody] IdDto userIdDto)
        {
            Id userId = Id.Create(userIdDto.Id).Value;

            Result endTrainingResult = await _trainingService.EndTrainingAsync(userId);

            return OkOrError(endTrainingResult);
        }

        [HttpPost(ApiRoutes.Trainings.TrainingData)]
        public async Task<IActionResult> AppendTrainingData([FromBody] AppendTrainingDataDto appendTrainingDataDto)
        {
            Id trainingId = Id.Create(appendTrainingDataDto.TrainingId).Value;
            var trainingRecords = Mapper.Map<List<TrainingRecord>>(appendTrainingDataDto.TrainingRecords);

            var trainingData = new TrainingData(trainingId, trainingRecords);

            Result appendTrainingDataResult = await _trainingService.AppendTrainingDataAsync(trainingData);

            return OkOrError(appendTrainingDataResult);
        }

        [HttpGet(ApiRoutes.Trainings.UserTrainings)]
        public async Task<IActionResult> GetTrainings([FromBody] GetTrainingsForUserParametersDto request)
        {
            Result<Id> idResult = Id.Create(request.UserId);

            if (idResult.IsFailure)
            {
                return Error(idResult);
            }

            Result<IEnumerable<TrainingData>> trainingsResult = await _trainingService.GetTrainings(
                new GetTrainingsForUserParameters(
                    idResult.Value,
                    request.PageNumber,
                    request.PageSize));

            return OkOrError<IEnumerable<TrainingDataDto>, IEnumerable<TrainingData>>(trainingsResult);
        }

        [HttpGet(ApiRoutes.Trainings.GetTrainingData)]
        public async Task<IActionResult> GetTrainingData(string id)
        {
            Result<Id> idResult = Id.Create(id);

            if (idResult.IsFailure)
            {
                return Error(idResult);
            }

            Result<TrainingData> trainingDataResult = await _trainingService.GetTraining(idResult.Value);

            return OkOrError<TrainingDataDto, TrainingData>(trainingDataResult);
        }
    }
}