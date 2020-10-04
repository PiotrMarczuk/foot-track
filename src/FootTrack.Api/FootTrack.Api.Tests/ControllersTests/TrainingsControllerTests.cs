using AutoMapper;
using FootTrack.Api.Controllers.V1;
using FootTrack.Api.Dtos.Requests;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services;
using FootTrack.Shared;
using MongoDB.Bson;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace FootTrack.Api.Tests.ControllersTests
{
    [TestFixture]
    public class TrainingsControllerTests
    {
        private IMapper _mapper;
        private ITrainingService _trainingService;
        private TrainingsController _sut;

        [SetUp]
        public void SetUp()
        {
            _mapper = Substitute.For<IMapper>();
            _trainingService = Substitute.For<ITrainingService>();

            _sut = new TrainingsController(_mapper, _trainingService);
        }

        [Test]
        public async Task When_training_already_exist_should_result_in_Conflict()
        {
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
            var dto = new IdDto { Id = id.Value };
            _trainingService.StartTrainingAsync(id).Returns(Result.Fail(Errors.Training.AlreadyStarted()));
            var result = await _sut.Start(dto) as ObjectResult;

            Assert.That((HttpStatusCode)result?.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task When_failed_to_start_training_because_of_unavailable_external_services_should_return_ServiceUnavailableCode()
        {
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
            var dto = new IdDto { Id = id.Value };
            _trainingService.StartTrainingAsync(id).Returns(Result.Fail(Errors.Training.FailedToStartTraining()));

            var result = await _sut.Start(dto) as StatusCodeResult;

            Assert.That((HttpStatusCode)result?.StatusCode, Is.EqualTo(HttpStatusCode.ServiceUnavailable));
        }
    }
}
