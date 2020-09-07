using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FootTrack.Api.Dtos.Requests;
using FootTrack.Api.Dtos.Responses;
using FootTrack.Api.IntegrationTests.Utils;
using FootTrack.BusinessLogic.Models.ValueObjects;
using MongoDB.Bson;
using Newtonsoft.Json;
using NUnit.Framework;

namespace FootTrack.Api.IntegrationTests
{
    [TestFixture]
    public class UsersControllerIntegrationTests
    {
        private ApiTestFixture _fixture;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            _fixture = new ApiTestFixture();
            _client = _fixture.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _fixture.Dispose();
            _client.Dispose();
        }

        [Test]
        public async Task Should_return_unauthorized_when_trying_to_get_user_data()
        {
            // ACT
            HttpResponseMessage httpResponse = await _client.GetAsync(Contracts.V1.ApiRoutes.Users.GetById);

            // ASSERT
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Should_register_new_user()
        {
            // ACT
            HttpResponseMessage httpResponse = await RegisterUser();

            // ASSERT
            Assert.That(httpResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task Should_be_able_to_get_user_data_when_authenticated()
        {
            // ARRANGE
            var apiResponse = await RegisterUserAndSetClientToken();

            // ACT
            HttpResponseMessage userResult = await GetUser(apiResponse.Result.Id);

            // ASSERT
            Assert.That(userResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Should_not_be_able_to_get_user_data_when_passing_wrong_token()
        {
            // ARRANGE
            var apiResponse = await RegisterUserAndSetClientToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "blablablablabla");

            // ACT
            HttpResponseMessage userResult = await GetUser(apiResponse.Result.Id);

            // ASSERT
            Assert.That(userResult.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Test]
        public async Task Should_return_NotFound_when_getting_user_with_not_existing_id()
        {
            // ARRANGE
            await RegisterUserAndSetClientToken();
            Id id = Id.Create(ObjectId.GenerateNewId().ToString()).Value;

            // ACT
            HttpResponseMessage userResult = await GetUser(id);

            // ASSERT
            Assert.That(userResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task When_trying_to_register_user_with_existing_email_should_return_Conflict()
        {
            await RegisterUser();
            HttpResponseMessage secondRegisterResult = await RegisterUser();

            Assert.That(secondRegisterResult.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));
        }

        [Test]
        public async Task When_trying_to_register_with_wrong_email_should_return_BadRequest()
        {
            HttpResponseMessage registerResult = await RegisterUser("wrongEmail");

            Assert.That(registerResult.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        private async Task<ApiResponse<AuthenticatedUserDto>> RegisterUserAndSetClientToken()
        {
            HttpResponseMessage registerResponse = await RegisterUser();
            string responseContent = await registerResponse.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<AuthenticatedUserDto>>(responseContent);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiResponse.Result.Token);
            return apiResponse;
        }

        private async Task<HttpResponseMessage> GetUser(string id)
        {
            string endpoint =
                Contracts.V1.ApiRoutes.Users.GetById.Substring(0,
                    Contracts.V1.ApiRoutes.Users.GetById.IndexOf("{", StringComparison.Ordinal));
            return await _client.GetAsync($"{endpoint}{id}");
        }

        private async Task<HttpResponseMessage> RegisterUser(string email = "test@gmail.com")
        {
            var user = new UserRegisterDto
            {
                Email = email,
                FirstName = "Kaziu",
                LastName = "Wichura",
                Password = "verystrongpassword"
            };

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            return await _client.PostAsync(Contracts.V1.ApiRoutes.Users.Register, content);
        }
    }
}