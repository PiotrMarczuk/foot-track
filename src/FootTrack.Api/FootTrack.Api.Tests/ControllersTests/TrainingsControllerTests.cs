using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FootTrack.Api.Controllers.V1;
using FootTrack.Api.Dtos.Requests;
using FootTrack.BusinessLogic.Models.ValueObjects;
using FootTrack.BusinessLogic.Services;
using FootTrack.Shared;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NSubstitute;
using NUnit.Framework;

namespace FootTrack.Api.UnitTests.ControllersTests
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

            if (result?.StatusCode != null)
            {
                Assert.That((HttpStatusCode) result.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
                return;
            }
            Assert.Fail();
        }

        [Test]
        public async Task When_failed_to_start_training_because_of_unavailable_external_services_should_return_ServiceUnavailableCode()
        {
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;
            var dto = new IdDto { Id = id.Value };
            _trainingService.StartTrainingAsync(id).Returns(Result.Fail(Errors.Training.FailedToStartTraining()));

            var result = await _sut.Start(dto) as StatusCodeResult;

            if (result?.StatusCode != null)
            {
                Assert.That((HttpStatusCode) result.StatusCode, Is.EqualTo(HttpStatusCode.ServiceUnavailable));
                return;
            }
            Assert.Fail();
        }
    }
}
