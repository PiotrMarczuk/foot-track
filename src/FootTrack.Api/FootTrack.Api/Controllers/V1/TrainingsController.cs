using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FootTrack.Api.Contracts.V1;
using FootTrack.Api.Dtos.Requests;
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

        [HttpPost(ApiRoutes.Trainings.AppendTrainingData)]
        public async Task<IActionResult> AppendTrainingData([FromBody] TrainingDataDto trainingDataDto)
        {
            Id userId = Id.Create(trainingDataDto.UserId).Value;
            var trainingRecords = Mapper.Map<List<TrainingRecord>>(trainingDataDto.TrainingRecords);
            
            var trainingData = new TrainingData(userId, trainingRecords);

            Result appendTrainingDataResult = await _trainingService.AppendTrainingDataAsync(trainingData);

            return OkOrError(appendTrainingDataResult);
        }
    }
}